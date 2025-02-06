using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Collections.Generic;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoutiqueController : ControllerBase
{
    private static readonly List<BoutiqueFeatured> _featuredBoutiques = new()
    {
        new BoutiqueFeatured
        {
            Id = "1",
            Name = "Everest Threads",
            ImageUrl = "https://images.pexels.com/photos/3756048/pexels-photo-3756048.jpeg",
            LogoUrl = "https://images.pexels.com/photos/3756048/pexels-photo-3756048.jpeg?auto=compress&cs=tinysrgb&h=150",
        },
        new BoutiqueFeatured
        {
            Id = "2",
            Name = "Urban Vogue",
            ImageUrl = "https://images.pexels.com/photos/6230786/pexels-photo-6230786.jpeg",
            LogoUrl = "https://images.pexels.com/photos/6230786/pexels-photo-6230786.jpeg?auto=compress&cs=tinysrgb&h=150",
        },
        // Add more featured boutiques as needed
    };

    [HttpGet("featured")]
    public ActionResult<IEnumerable<BoutiqueFeatured>> GetFeaturedBoutiques()
    {
        return Ok(_featuredBoutiques);
    }

    // [HttpGet("boutiquesWithInventory")]
    // public ActionResult<IEnumerable<BoutiqueWithInventory>> GetBoutiquesWithInventory()
    // {
    //     return Ok(_boutiquesWithInventory);
    // }
} 