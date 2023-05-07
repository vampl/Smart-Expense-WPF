using System;
using System.Collections.ObjectModel;
using System.Windows;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;
using SmartExpense.MVVM.View;

namespace SmartExpense.MVVM.ViewModel;

public class AccountViewModel : ObservableObject
{
    // 
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
    }
    
    // Команда для виклику операції додання рядка в DataGrid елементу
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        new AccountFormView(AddNewAccount).Show();
    }
    
    private void AddNewAccount(AccountModel account)
    {
        try
        {
            Accounts.Add(account);
            
            // var applicationContext = new ApplicationContext();
            // applicationContext.Transactions.Add(transaction);
            // applicationContext.SaveChanges();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
        
    }

    public AccountViewModel()
    {
        var applicationContext = new ApplicationContext();
        
        Accounts = applicationContext.GetUserAccounts("10000001");
    }
}