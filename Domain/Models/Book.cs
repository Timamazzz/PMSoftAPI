using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string? Description { get; set; }
    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }
    public int? AuthorId { get; set; }
    public Author? Author { get; set; }
    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public int Rating { get; set; }
    public string? ImagePreview { get; set; }
}