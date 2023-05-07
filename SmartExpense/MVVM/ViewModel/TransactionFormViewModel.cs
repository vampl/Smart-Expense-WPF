using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;

namespace SmartExpense.MVVM.ViewModel;

/* ToDo: створити механізм передачі заповненої форми до таблиці та додати можливість пересування*/
public class TransactionFormViewModel : TransactionViewModel
{
    private const string TEXT_INCOME_TYPE_DEFINOTION = "Надходження";
    private const string TEXT_OUTCOME_TYPE_DEFINOTION = "Витрата";
    
    public List<string> AccountsTitle { get; set; }
    public List<string> TransactionTypes { get; set; }

    public TransactionFormViewModel()
    {
        var applicationContext = new ApplicationContext();

        TransactionTypes = new List<string> { TEXT_INCOME_TYPE_DEFINOTION, TEXT_OUTCOME_TYPE_DEFINOTION };
        AccountsTitle = applicationContext.GetUserAccounts("10000001").Select(account => account.Title).ToList();
    }
}