using Application.Abstractions;
using Bogus;
using Core.ValueObjects;
using Dapper;

namespace API.Extensions;

public static class SeedDataExtensions
{
    public static void AddSeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        // first, check if we already have users in the database
        var userCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users");
        if (userCount == 0)
        {
            // Create fake users
            var users = new Faker<Core.Entities.User>()
                .CustomInstantiator(f =>
                {
                    var user = Core.Entities.User.Create(
                        id: f.IndexFaker + 1,
                        firstName: f.Person.FirstName,
                        lastName: f.Person.LastName,
                        email: Email.Create(f.Internet.Email())
                    );
                    user.SetIdentityId($"{f.IndexFaker + 1}_{f.Person.FirstName}_{f.Person.LastName}_{f.Internet.Email()}");
                    return user;
                })
                .Generate(10);

            // Insert users into database
            var userSql = @"INSERT INTO Users (FirstName, LastName, Email, IdentityId, CreatedAt, CreatedBy, LastModifiedAt, LastModifiedBy)
                VALUES (@FirstName, @LastName, @Email, @IdentityId, @CreatedAt, @CreatedBy, @LastModifiedAt, @LastModifiedBy)";

            foreach (var user in users)
            {
                connection.Execute(userSql, new
                {
                    user.FirstName,
                    user.LastName,
                    Email = user.Email.Value, // convert the email to a string
                    user.IdentityId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    LastModifiedAt = DateTime.UtcNow,
                    LastModifiedBy = "Seed"
                });
            }
        }

        // Get user IDs directly from the database without deserializing
        var userIds = connection.Query<int>("SELECT Id FROM Users").ToList();

        // Check if boutiques already exist
        var boutiqueCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Boutiques");
        if (boutiqueCount < 10)
        {
            // Create fake boutiques
            var boutiques = new Faker<Core.Entities.Boutique>()
                .CustomInstantiator(f => Core.Entities.Boutique.Create(
                    id: f.IndexFaker + 1,
                    ownerId: f.PickRandom(userIds),
                    name: f.Company.CompanyName(),
                    profilePicture: $"https://source.unsplash.com/random/500x500/?fashion, boutique",
                    followers: f.Random.Int(0, 10000),
                    description: f.Lorem.Paragraph(),
                    category: f.Commerce.Categories(1)[0],
                    location: f.Address.City(),
                    contact: f.Phone.PhoneNumber(),
                    instagramLink: f.Internet.Url()
                ))
                .Generate(10);

            // Insert boutiques into database
            var sql = @"INSERT INTO Boutiques (OwnerId, Name, ProfilePicture, Followers, Description, Category, Location, Contact, InstagramLink, CreatedAt, CreatedBy, LastModifiedAt, LastModifiedBy, Version)
                VALUES (@OwnerId, @Name, @ProfilePicture, @Followers, @Description, @Category, @Location, @Contact, @InstagramLink, @CreatedAt, @CreatedBy, @LastModifiedAt, @LastModifiedBy, @Version)";

            foreach (var boutique in boutiques)
            {
                connection.Execute(sql, new
                {
                    boutique.OwnerId,
                    boutique.Name,
                    boutique.ProfilePicture,
                    boutique.Followers,
                    boutique.Description,
                    boutique.Category,
                    boutique.Location,
                    boutique.Contact,
                    boutique.InstagramLink,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "Seed",
                    LastModifiedAt = DateTime.UtcNow,
                    LastModifiedBy = "Seed",
                    Version = 1
                });
            }

        }

        // Get boutique IDs directly without deserializing
        var boutiqueIds = connection.Query<int>("SELECT Id FROM Boutiques").ToList();
        // Create 5 inventory items for each boutique
        var inventoryItems = new List<Core.Entities.InventoryItem>();
        foreach (var boutiqueId in boutiqueIds)
        {
            var boutiqueInventoryItems = new Faker<Core.Entities.InventoryItem>()
                .CustomInstantiator(f => Core.Entities.InventoryItem.Create(
                    id: f.IndexFaker + 1,
                    boutiqueId: boutiqueId,
                    imageUrl: $"https://source.unsplash.com/random/500x500/?fashion,clothing",
                    caption: f.Commerce.ProductDescription()
                ))
                .Generate(5);

            inventoryItems.AddRange(boutiqueInventoryItems);
        }

        // Insert inventory items into database
        var inventorySql = @"INSERT INTO InventoryItems (BoutiqueId, ImageUrl, Caption, CreatedAt, CreatedBy, LastModifiedAt, LastModifiedBy)
                VALUES (@BoutiqueId, @ImageUrl, @Caption, @CreatedAt, @CreatedBy, @LastModifiedAt, @LastModifiedBy)";

        foreach (var item in inventoryItems)
        {
            connection.Execute(inventorySql, new
            {
                item.BoutiqueId,
                item.ImageUrl,
                item.Caption,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seed",
                LastModifiedAt = DateTime.UtcNow,
                LastModifiedBy = "Seed"
            });
        }

        // Create fake social posts
        var socialPosts = new List<Core.Entities.SocialPost>();
        foreach (var boutiqueId in boutiqueIds)
        {
            var boutiqueSocialPosts = new Faker<Core.Entities.SocialPost>()
                .CustomInstantiator(f => Core.Entities.SocialPost.Create(
                    id: f.IndexFaker + 1,
                    boutiqueId: boutiqueId,
                    username: f.Person.FullName,
                    comment: f.Lorem.Sentence(),
                    timestamp: f.Date.Past()
                ))
                .Generate(3);

            socialPosts.AddRange(boutiqueSocialPosts);
        }

        // Insert social posts into database
        var socialPostSql = @"INSERT INTO SocialPosts (BoutiqueId, Username, Comment, Timestamp, CreatedAt, CreatedBy, LastModifiedAt, LastModifiedBy)
                VALUES (@BoutiqueId, @Username, @Comment, @Timestamp, @CreatedAt, @CreatedBy, @LastModifiedAt, @LastModifiedBy)";

        foreach (var post in socialPosts)
        {
            connection.Execute(socialPostSql, new
            {
                post.BoutiqueId,
                post.Username,
                post.Comment,
                post.Timestamp,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seed",
                LastModifiedAt = DateTime.UtcNow,
                LastModifiedBy = "Seed"
            });
        }
    }
}
