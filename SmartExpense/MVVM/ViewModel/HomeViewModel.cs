using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SmartExpense.Core;

namespace SmartExpense.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public SeriesCollection IncomeOutcomeSeriesCollection { get; set; }

    private decimal _income;
    private decimal _outcome;
    private decimal _balance;
    
    public object? Income
    {
        get => _income;
        set
        {
            _income = (decimal)value!;
            OnPropertyChanged();
        }
    }
    
    public object? Outcome
    {
        get => _outcome;
        set
        {
            _outcome = (decimal)value!;
            OnPropertyChanged();
        }
    }
    
    public object? Balance
    {
        get => _balance;
        set
        {
            _balance = (decimal)value!;
            OnPropertyChanged();
        }
    }

    public HomeViewModel()
    {
        _income = 1000.0M;
        _outcome = 435.0M;
        _balance = 17000.20M;
        
        IncomeOutcomeSeriesCollection = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Надходження",
                Values = new ChartValues<ObservableValue>{ new((double)_income) },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "Витрати",
                Values = new ChartValues<ObservableValue>{ new((double)_outcome) },
                DataLabels = true
            }
        };
    }
}