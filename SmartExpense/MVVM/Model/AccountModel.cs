using System.ComponentModel.DataAnnotations;

namespace SmartExpense.Core.Database.DataModels;

public class AccountModel
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Discription { get; set; }
    public decimal Amount { get; set; }
    [Required] public UserModel? User { get; set; }
}