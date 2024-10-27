using System.Text.Json;
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
    public IActionResult AddBook([FromBody] Book book)
    {
        try
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return BadRequest();
        }
        return Ok();
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
            return NotFound();
        return Ok(JsonSerializer.Serialize(book));
    }
    
    [HttpPut]
    [Route("books/{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book bookData)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == id);
        if (book is null)
            return NotFound();
        try
        {
            book.Title = bookData.Title;
            book.Description = bookData.Description;
            book.Text = bookData.Text;
            book.AuthorId = bookData.AuthorId;
            _db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpDelete]
    [Route("books/{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == id);
        if (book is null) 
            return NotFound();
        _db.Books.Remove(book);
        _db.SaveChanges();
        return Ok();
    }
}