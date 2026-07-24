using System.ComponentModel.DataAnnotations;

namespace BelajarGitBlazor.Models;

public class Message
{
    public int Id { get; set; }
    
    [Required]
    public string SenderName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsRead { get; set; } = false;
}
