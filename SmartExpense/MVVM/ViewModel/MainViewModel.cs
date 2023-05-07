using System;
using System.Windows;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private UserModel _user = null!;
    protected UserModel User { get; set; } = new UserModel { Username = "10000001", Password = "10000001"};

    public RelayCommand HomeViewCommand { get; set; } = null!;
    public RelayCommand TransactionViewCommand { get; set; } = null!;
    public RelayCommand AccountViewCommand { get; set; } = null!;

    private HomeViewModel HomeVm { get; set; } = null!;
    private TransactionViewModel TransactionVm { get; set; } = null!;
    private AccountViewModel AccountVm { get; set; } = null!;

    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        try
        {
            HomeVm = new HomeViewModel();
            TransactionVm = new TransactionViewModel();
            AccountVm = new AccountViewModel();
        
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(_ => { CurrentView = HomeVm; });
            TransactionViewCommand = new RelayCommand(_ => { CurrentView = TransactionVm; });
            AccountViewCommand = new RelayCommand(_ => { CurrentView = AccountVm; });
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}