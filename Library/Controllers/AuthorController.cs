using System.Text.Json;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class AuthorController : Controller
{
    private ApplicationContext _db;
    
    public AuthorController(ApplicationContext context)
    {
        _db = context;
    }

    [HttpPost]
    [Route("authors")]
    public IActionResult AddAuthor([FromBody] Author author)
    {
        try
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpGet]
    [Route("authors")]
    public IActionResult GetAuthors()
    {
        var authors = _db.Authors;
        return Ok(JsonSerializer.Serialize(authors));
    }
}