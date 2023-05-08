using System.Collections.ObjectModel;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;
using SmartExpense.MVVM.View;

namespace SmartExpense.MVVM.ViewModel;

public class AccountViewModel : ObservableObject
{
    // Колекція усіх наявних в користувача рахунків
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

    // Команда для виклику операції видалення рядка із DataGrid елементу
    private RelayCommand? _rowRemoveCommand;
    public RelayCommand RowRemoveCommand =>
        _rowRemoveCommand ??= new RelayCommand(RemoveRow);

    private void RemoveRow(object parameter)
    {
        var rowIndex = Accounts.IndexOf((AccountModel)parameter);

        if (rowIndex > -1 && rowIndex < Accounts.Count)
            Accounts.RemoveAt(rowIndex);

        var applicationContext = new ApplicationContext();
        applicationContext.Accounts.Remove((AccountModel)parameter);
        applicationContext.SaveChanges();
    }
    
    // Команда для виклику операції додання рядка в DataGrid елементу
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        new AccountFormView().Show();
        var applicationContext = new ApplicationContext();
        Accounts = applicationContext.GetUserAccounts();
    }

    public AccountViewModel()
    {
        var applicationContext = new ApplicationContext();
        
        Accounts = applicationContext.GetUserAccounts();
    }
}