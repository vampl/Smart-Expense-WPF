using System;
using System.Collections.Generic;
using System.Linq;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class TransactionFormViewModel : ObservableObject
{
    private const string TEXT_INCOME_TYPE_DEFINOTION = "Надходження";
    private const string TEXT_OUTCOME_TYPE_DEFINOTION = "Витрата";

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

    private DateTime _creationDate;
    public DateTime CreationDate
    {
        get => _creationDate;
        set
        {
            _creationDate = value;
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
    
    private string _accountTitle;
    public string AccountTitle
    {
        get => _accountTitle;
        set
        {
            _accountTitle = value;
            OnPropertyChanged();
        } 
    }
    
    private RelayCommand? _rowAddCommand;
    public RelayCommand RowAddCommand =>
        _rowAddCommand ??= new RelayCommand(AddRow);
    
    private void AddRow(object parameter)
    {
        var applicationContext = new ApplicationContext();
        var transactionModel = new TransactionModel
        {
            Amount = Amount,
            Type = Type,
            CreationDate = CreationDate,
            Description = Description,
            AccountTitle = AccountTitle,
            UserId = applicationContext.GetUser().Id
        };
        
        applicationContext.AddTransaction(transactionModel);
    }

    public List<string> AccountsTitle { get; set; }
    public List<string> TransactionTypes { get; set; }

    public TransactionFormViewModel()
    {
        var applicationContext = new ApplicationContext();

        TransactionTypes = new List<string> { TEXT_INCOME_TYPE_DEFINOTION, TEXT_OUTCOME_TYPE_DEFINOTION };
        AccountsTitle = applicationContext.GetUserAccounts().Select(account => account.Title).ToList();
    }
}