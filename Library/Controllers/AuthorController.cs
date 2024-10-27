using System.Text.Json;
using Library.Dto;
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
    public IActionResult AddAuthor([FromBody] AuthorDto authorDto)
    {
        var author = new Author()
        {
            Name = authorDto.Name, 
            Surname = authorDto.Surname, 
            Patronymic = authorDto.Patronymic
        };
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