using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartExpense.MVVM.Model;

public class TransactionModel
{
    [Key] 
    public long Id { get; set; }
    [Required] 
    [Precision(14, 2)]
    public decimal Amount { get; set; }
    [Required] 
    public string Type { get; set; }
    [Required] 
    public DateTime CreationDate { get; set; }
    public string? Description { get; set; }
    [Required] 
    public string AccountTitle { get; set; }
    
    [Required] 
    [ForeignKey("User"), Column(Order = 2)] 
    public long UserId { get; set; }
    public UserModel User { get; set; }
}