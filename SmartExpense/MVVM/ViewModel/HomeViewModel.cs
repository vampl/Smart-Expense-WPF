using System;
using System.Collections.ObjectModel;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    private const string TEXT_INCOME_TYPE_DEFINOTION = "Надходження";
    private const string TEXT_OUTCOME_TYPE_DEFINOTION = "Витрата";
    
    // "Пирогова" діаграма
    public SeriesCollection PieSeriesCollection { get; set; } = null!;

    // Рядок підсумованих надходжень
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

    // Рядок підсумованих витрат
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

    // Рядок підсумованого балансу
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

    public HomeViewModel()
    {
        try
        {
            var applicationContext = new ApplicationContext();

           Transactions = applicationContext.GetUserTransactions();
            Income = applicationContext.GetUserIncome();
            Outcome = applicationContext.GetUserOutcome();
            Balance = applicationContext.GetUserBalance();

            PieSeriesCollection = CreatePieChartFromData();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private SeriesCollection CreatePieChartFromData() =>
        new()
        {
            new PieSeries
            {
                Title = "Надходження", Values = new ChartValues<ObservableValue> { new((double)Income) }, DataLabels = true
            },
            new PieSeries
            {
                Title = "Витрати", Values = new ChartValues<ObservableValue> { new((double)Outcome) }, DataLabels = true
            }
        };
}