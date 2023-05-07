using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartExpense.MVVM.Model;

[PrimaryKey("Id")]
[Index(nameof(Username), IsUnique = true)]
public class UserModel
{
    [Key, Column(Order = 1)]
    [MaxLength(8),MinLength(8)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Key, Column(Order = 2)]
    [MaxLength(12),MinLength(4)]
    public string Username { get; set; }
    [Required] 
    [MaxLength(12), MinLength(4)]
    public string Password { get; set; }
    
    public ICollection<TransactionModel> Transactions { get; set; }
    public ICollection<AccountModel> Accounts { get; set; }
}