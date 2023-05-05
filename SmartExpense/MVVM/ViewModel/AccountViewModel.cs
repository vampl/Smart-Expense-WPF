using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;
using SmartExpense.Core.Database;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class AccountViewModel : ObservableObject
{
    private ApplicationContext _context;
    private List<AccountModel> _accountsList;
    
    public List<AccountModel> AccountsList
    {
        get => _accountsList;
        set
        {
            _accountsList = value;
            OnPropertyChanged();
        }
    }
    
    public AccountViewModel()
    {
        _context = new ApplicationContext();
    }
}