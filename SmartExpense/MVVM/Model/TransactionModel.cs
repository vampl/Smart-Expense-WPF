using System;
using System.ComponentModel.DataAnnotations;

namespace SmartExpense.Core.Database.DataModels;

public class TransactionModel
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Discription { get; set; }
    public string AccountTitle { get; set; }
    [Required] public UserModel? User { get; set; }
}