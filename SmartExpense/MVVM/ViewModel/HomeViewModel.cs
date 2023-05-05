using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SmartExpense.Core;
using SmartExpense.Core.Database;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    private ApplicationContext _context;
    
    public SeriesCollection PieSeriesCollection { get; set; }

    private decimal _income;
    private decimal _outcome;
    private decimal _balance;

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

    public HomeViewModel()
    {
        _context = new ApplicationContext();
        var userTransactions = _context.Transactions.Where(x => x.OwnerId == 10000001).ToList();

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