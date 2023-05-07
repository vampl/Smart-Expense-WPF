using System;
using System.Collections.ObjectModel;
using System.Windows;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;
using SmartExpense.MVVM.View;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionViewModel : ObservableObject
{
    // Список транзакцій користувача із реалізацією повернення/призвоєння.
    private ObservableCollection<TransactionModel> _transactions = null!;
    public ObservableCollection<TransactionModel> Transactions
    {
        get => _transactions;
        set
        {
            _transactions = value;
            OnPropertyChanged();
        }
    }

    // Команда для виклику операції видалення рядка із DataGrid елементу
    private RelayCommand? _rowRemoveCommand;
    public RelayCommand RowRemoveCommand =>
        _rowRemoveCommand ??= new RelayCommand(RemoveRow);

    private void RemoveRow(object parameter)
    {
        var rowIndex = Transactions.IndexOf((TransactionModel)parameter);

        if (rowIndex > -1 && rowIndex < Transactions.Count)
            Transactions.RemoveAt(rowIndex);
    }
    
    // Команда для виклику операції додання рядка в DataGrid елементу
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        new TransactionFormView(AddNewTransaction).Show();
    }
    
    private void AddNewTransaction(TransactionModel transaction)
    {
        try
        {
            Transactions.Add(transaction);
            
            // var applicationContext = new ApplicationContext();
            // applicationContext.Transactions.Add(transaction);
            // applicationContext.SaveChanges();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
        
    }


    public TransactionViewModel()
    {
        var applicationContext = new ApplicationContext();

        Transactions = applicationContext.GetUserTransactions("10000001");
    }
}