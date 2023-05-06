using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionFormViewModel
{
    private ApplicationContext _context;

    public List<string> TransactionTypes { get; set; }
    public List<string> AccountsTitle { get; set; }

    public TransactionFormViewModel()
    {
        TransactionTypes = new List<string>
        {
            new string("Надходження"),
            new string("Витрата")
        };

        _context = new ApplicationContext();
        AccountsTitle = _context.Accounts.Where(x => x.User.UserName == "10000001").Select(x => x.Title).ToList();
    }
}