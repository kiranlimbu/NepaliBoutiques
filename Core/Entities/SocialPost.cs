using System.Reflection;
using Core.Abstractions;
using Core.Events;
namespace Core.Entities;

public sealed class SocialPost : BaseEntity
{
    public int BoutiqueId { get; private set; }
    // user who posted the comment
    public string Username { get; private set; }
    public string Comment { get; private set; }
    public DateTime Timestamp { get; private set; }

    private SocialPost(int id, int boutiqueId, string username, string comment, DateTime timestamp) : base(id)
    {
        BoutiqueId = boutiqueId;
        Username = username;
        Comment = comment;
        Timestamp = timestamp;
    }

    public static SocialPost Create(int id, int boutiqueId, string username, string comment, DateTime timestamp)
    {
        var socialPost = new SocialPost(id, boutiqueId, username, comment, timestamp);
        return socialPost;
    }
}