using System.Collections.Generic;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class AccountFormViewModel : ObservableObject
{
    private const string TEXT_CASH_ACCOUNT_DEFINOTION = "Готівка";
    private const string TEXT_CARD_ACCOUNT_DEFINOTION = "Карта";
    
    public List<string> AccountsTypes { get; set; }
    
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        } 
    }
    
    private string _type;
    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged();
        } 
    }

    private string _description;
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        } 
    }
    
    private decimal _amount;
    public decimal Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            OnPropertyChanged();
        } 
    }
    
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        var applicationContext = new ApplicationContext();
        applicationContext.Accounts.Add(new AccountModel
        {
            Title = Title,
            Type = Type,
            Description = Description,
            Amount = Amount,
            UserId = applicationContext.GetUser().Id
        });
        
        applicationContext.SaveChanges();
    }

    public AccountFormViewModel()
    {
        AccountsTypes = new List<string> { TEXT_CASH_ACCOUNT_DEFINOTION, TEXT_CARD_ACCOUNT_DEFINOTION };
    }
}