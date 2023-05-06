using System.ComponentModel.DataAnnotations;

namespace SmartExpense.MVVM.Model;

public class AccountModel
{
    [Required] public UserModel? User { get; set; }
    public long Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Discription { get; set; }
    public decimal Amount { get; set; }
}