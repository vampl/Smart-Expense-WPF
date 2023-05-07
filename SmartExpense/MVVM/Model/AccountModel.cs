using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartExpense.MVVM.Model;

public class AccountModel
{
    [Key] 
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Type { get; set; }
    public string? Description { get; set; }
    [Required]
    [Precision(14, 2)]
    public decimal Amount { get; set; }
    
    [Required]
    [ForeignKey("User"), Column(Order = 2)] 
    public long UserId { get; set; }
    public UserModel User { get; set; }
}