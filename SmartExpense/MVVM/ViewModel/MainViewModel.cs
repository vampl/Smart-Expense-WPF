using SmartExpense.Core;
using SmartExpense.MVVM.View;

namespace SmartExpense.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand TransactionViewCommand { get; set; }
    public RelayCommand AccountViewCommand { get; set; }
    
    public HomeViewModel HomeVm { get; set; }
    public TransactionViewModel TransactionVm { get; set; }
    public AccountViewModel AccountVm { get; set; }

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
        HomeVm = new HomeViewModel();
        TransactionVm = new TransactionViewModel();
        AccountVm = new AccountViewModel();
        
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(_ => { CurrentView = HomeVm; });
        TransactionViewCommand = new RelayCommand(_ => { CurrentView = TransactionVm; });
        AccountViewCommand = new RelayCommand(_ => { CurrentView = AccountVm; });
    }
}