using System;
using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;
using SmartExpense.Core.Database.DataModels;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionViewModel : ObservableObject
{
    private List<Transaction> _transactionsList;
    
    public List<Transaction> TransactionList
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
        var transactions = new List<Transaction>
        {
            new() { OwnerId = 10000001, Amount = 100.20M, Type = "Витрата", CreationDate = DateTime.Today.Date, Discription = "Buy some drink", AccountTitle = "Credit Card"},
            new() { OwnerId = 10000001, Amount = 1290.80M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "Wage", AccountTitle = "Card"},
            new() { OwnerId = 10000002, Amount = 100.20M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "But some drink", AccountTitle = "Credit Card"},
            new() { OwnerId = 10000003, Amount = 100.20M, Type = "Витрата", CreationDate = DateTime.Today.Date, AccountTitle = "Credit Cart"},
            new() { OwnerId = 10000001, Amount = 10.0M, Type = "Витрата", CreationDate = DateTime.Today.Date, Discription = "Tic-tac", AccountTitle = "Wallet"},
            new() { OwnerId = 10000001, Amount = 100.20M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "But some drink", AccountTitle = "Card"},
        };
        
        var userTransactions = transactions.Where(x => x.OwnerId == 10000001).ToList();
        TransactionList = userTransactions;
    }
}