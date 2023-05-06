using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionViewModel : ObservableObject
{
    private ApplicationContext _context;

    private ObservableCollection<TransactionModel> _transactions;

    public ObservableCollection<TransactionModel> Transaction
    {
        get => _transactions;
        set
        {
            _transactions = value;
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
        var index = Transaction.IndexOf(parameter as TransactionModel);
        if (index > -1 && index < Transaction.Count)
        {
            Transaction.RemoveAt(index);
        }
    }
    
    private bool CanRemoveRow(object parameter)
    {
        return true;
    }

    public TransactionViewModel()
    {
        _context = new ApplicationContext();
        
        var userTransactions = _context.Transactions.Where(x => x.User.UserName == "10000001").ToList();
        Transaction = new ObservableCollection<TransactionModel>(userTransactions);
    }
}