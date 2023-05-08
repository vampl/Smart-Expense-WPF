using System.Collections.ObjectModel;
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

        var applicationContext = new ApplicationContext();
        var transactionModel = (TransactionModel)parameter;
        applicationContext.DeleteTransaction(transactionModel);
    }
    
    // Команда для виклику операції додання рядка в DataGrid елементу
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        new TransactionFormView().Show();
        var applicationContext = new ApplicationContext();
        Transactions = applicationContext.GetUserTransactions();
    }
    
    public TransactionViewModel()
    {
        var applicationContext = new ApplicationContext();

        Transactions = applicationContext.GetUserTransactions();
    }
}