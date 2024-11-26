using System.Text.Json;
using Library.Dto;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

public class BookController : Controller
{
    private ApplicationContext _db;
    
    public BookController(ApplicationContext context)
    {
        _db = context;
    }

    [HttpPost]
    [Route("books")]
    public IActionResult AddBook([FromBody] BookDto bookDto)
    {
        var book = new Book()
        {
            Title = bookDto.Title, 
            Description = bookDto.Description, 
            Text = bookDto.Text, 
            AuthorId = bookDto.AuthorId
        };
        try
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return BadRequest("Invalid input");
        }
        return Ok("Book created successfully");
    }
    
    [HttpGet]
    [Route("books")]
    public IActionResult GetBooks()
    {
        var books = _db.Books;
        return Ok(JsonSerializer.Serialize(books));
    }
    
    [HttpGet]
    [Route("books/{id}")]
    public IActionResult GetBook(int id)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == id);
        if (book is null)
            return NotFound("Book not found");
        return Ok(JsonSerializer.Serialize(book));
    }
    
    [HttpPut]
    [Route("books/{id}")]
    public IActionResult UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == id);
        if (book is null)
            return NotFound("Book not found");
        try
        {
            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Text = bookDto.Text;
            book.AuthorId = bookDto.AuthorId;
            _db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return BadRequest("Invalid input");
        }
        return Ok("Book updated successfully");
    }
    
    [HttpDelete]
    [Route("books/{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == id);
        if (book is null) 
            return NotFound("Book not found");
        _db.Books.Remove(book);
        _db.SaveChanges();
        return Ok("Book deleted successfully");
    }
}