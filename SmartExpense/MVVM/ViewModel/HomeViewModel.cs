using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    private ApplicationContext _context;
    
    public SeriesCollection PieSeriesCollection { get; set; }

    private decimal _income;
    public decimal Income
    {
        get => _income;
        set
        {
            _income = value!;
            OnPropertyChanged();
        }
    }
    
    private decimal _outcome;
    public decimal Outcome
    {
        get => _outcome;
        set
        {
            _outcome = value!;
            OnPropertyChanged();
        }
    }
    
    private decimal _balance;
    public decimal Balance
    {
        get => _balance;
        set
        {
            _balance = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<TransactionModel> _transactionsList;
    public ObservableCollection<TransactionModel> TransactionList
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
        var userTransactions = _context.Transactions.Where(x => x.User.UserName == "10000001").ToList();

        Income = userTransactions.Where(x => x.Type == "Надходження").Sum(x=> x.Amount);
        Outcome = userTransactions.Where(x => x.Type == "Витрата").Sum(x=> x.Amount);
        Balance = Income - Outcome;
        
        TransactionList = new ObservableCollection<TransactionModel>(userTransactions);

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