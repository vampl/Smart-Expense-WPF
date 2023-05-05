using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SmartExpense.Core;
using SmartExpense.Core.Database.DataModels;

namespace SmartExpense.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public SeriesCollection PieSeriesCollection { get; set; }

    private decimal _income;
    private decimal _outcome;
    private decimal _balance;

    private List<Transaction> _transactionsList;

    public decimal Income
    {
        get => _income;
        set
        {
            _income = value!;
            OnPropertyChanged();
        }
    }
    
    public decimal Outcome
    {
        get => _outcome;
        set
        {
            _outcome = value!;
            OnPropertyChanged();
        }
    }
    
    public decimal Balance
    {
        get => _balance;
        set
        {
            _balance = value;
            OnPropertyChanged();
        }
    }
    
    public List<Transaction> TransactionList
    {
        get => _transactionsList;
        set
        {
            _transactionsList = value;
            OnPropertyChanged();
        }
    }

    public HomeViewModel()
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

        Income = userTransactions.Where(x => x.Type == "Надходження").Sum(x=> x.Amount);
        Outcome = userTransactions.Where(x => x.Type == "Витрата").Sum(x=> x.Amount);
        Balance = Income - Outcome;
        
        TransactionList = userTransactions;

        PieSeriesCollection = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Надходження",
                Values = new ChartValues<ObservableValue> { new((double)Income) },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "Витрати",
                Values = new ChartValues<ObservableValue> { new((double)Outcome) },
                DataLabels = true
            }
        };
    }
}