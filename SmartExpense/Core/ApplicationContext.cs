using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartExpense.Core.Database.DataModels;

namespace SmartExpense.Core.Database;

public class ApplicationContext : DbContext
{
    public List<TransactionModel> Transactions { get; set; }

    public ApplicationContext()
    {
        if (Transactions == null)
        {
            Transactions = new List<TransactionModel>
            {
                new() { OwnerId = 10000001, Amount = 100.20M, Type = "Витрата", CreationDate = DateTime.Today.Date, Discription = "Buy some drink", AccountTitle = "Credit Card"},
                new() { OwnerId = 10000001, Amount = 1290.80M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "Wage", AccountTitle = "Card"},
                new() { OwnerId = 10000002, Amount = 100.20M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "But some drink", AccountTitle = "Credit Card"},
                new() { OwnerId = 10000003, Amount = 100.20M, Type = "Витрата", CreationDate = DateTime.Today.Date, AccountTitle = "Credit Cart"},
                new() { OwnerId = 10000001, Amount = 10.0M, Type = "Витрата", CreationDate = DateTime.Today.Date, Discription = "Tic-tac", AccountTitle = "Wallet"},
                new() { OwnerId = 10000001, Amount = 100.20M, Type = "Надходження", CreationDate = DateTime.Today.Date, Discription = "But some drink", AccountTitle = "Card"},
            };
        }
    }
}