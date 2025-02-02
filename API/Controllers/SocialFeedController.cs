using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SocialFeedController : ControllerBase
{
    private static readonly List<SocialPost> _posts = new()
    {
        new SocialPost
        {
            Id = 1,
            UserName = "James",
            Comment = "I love this boutique. They have everything I want.",
            BoutiqueName = "Style Haven",
        },
        new SocialPost
        {
            Id = 2,
            UserName = "Sarah",
            Comment = "Great customer service and amazing collection!",
            BoutiqueName = "Urban Chic",
        },
        new SocialPost
        {
            Id = 3,
            UserName = "Michael",
            Comment = "Best boutique in town. Highly recommended!",
            BoutiqueName = "Elegance Plus",
        },
        new SocialPost
        {
            Id = 4,
            UserName = "Emma",
            Comment = "The quality of their clothes is outstanding.",
            BoutiqueName = "Style Haven",
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<SocialPost>> GetPosts()
    {
        return Ok(_posts);
    }

    [HttpGet("{id}")]
    public ActionResult<SocialPost> GetPost(int id)
    {
        var post = _posts.Find(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }
}