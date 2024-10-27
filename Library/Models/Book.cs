namespace Library.Models;

public class Book
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }

    public string Text { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}