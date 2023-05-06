using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class AccountViewModel : ObservableObject
{
    private ApplicationContext _context;

    private ObservableCollection<AccountModel> _accounts;

    public ObservableCollection<AccountModel> Accounts
    {
        get => _accounts;
        set
        {
            _accounts = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _mRowRemoveCommand;
    public RelayCommand RemoveCommand
    {
        get
        {
            if (_mRowRemoveCommand == null)
            {
                _mRowRemoveCommand = new RelayCommand(RemoveRow, CanRemoveRow);
            }
            return _mRowRemoveCommand;
        }
    }
    private void RemoveRow(object parameter)
    {
        var index = Accounts.IndexOf(parameter as AccountModel);
        if (index > -1 && index < Accounts.Count)
        {
            Accounts.RemoveAt(index);
        }
    }
    
    private bool CanRemoveRow(object parameter)
    {
        return true;
    }

    public AccountViewModel()
    {
        _context = new ApplicationContext();
        
        var userAccounts = _context.Accounts.Where(x => x.User.UserName == "10000001").ToList();
        Accounts = new ObservableCollection<AccountModel>(userAccounts);
    }
}