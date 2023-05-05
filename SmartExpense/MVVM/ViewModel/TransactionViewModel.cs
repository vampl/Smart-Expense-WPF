using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;
using SmartExpense.Core.Database;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionViewModel : ObservableObject
{
    private ApplicationContext _context;
    private List<TransactionModel> _transactionsList;
    
    public List<TransactionModel> TransactionList
    {
        get => _transactionsList;
        set
        {
            _transactionsList = value;
            OnPropertyChanged();
        }
    }
    
    public TransactionViewModel()
    {
        _context = new ApplicationContext();
        var userTransactions = _context.Transactions.Where(x => x.OwnerId == 10000001).ToList();
        TransactionList = userTransactions;
    }
}