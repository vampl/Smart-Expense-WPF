using System.Collections.Generic;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.ViewModel;

public class AccountFormViewModel : AccountModel
{
    private const string TEXT_CASH_ACCOUNT_DEFINOTION = "Готівка";
    private const string TEXT_CARD_ACCOUNT_DEFINOTION = "Карта";
    
    public List<string> AccountsTypes { get; set; }

    public AccountFormViewModel()
    {
        AccountsTypes = new List<string> { TEXT_CASH_ACCOUNT_DEFINOTION, TEXT_CARD_ACCOUNT_DEFINOTION };
    }
}