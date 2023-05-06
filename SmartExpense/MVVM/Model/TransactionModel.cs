using System;
using System.ComponentModel.DataAnnotations;

namespace SmartExpense.MVVM.Model;

public class TransactionModel
{
    [Required] public UserModel? User { get; set; }
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Discription { get; set; }
    public string AccountTitle { get; set; }
}