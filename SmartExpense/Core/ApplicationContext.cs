using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SmartExpense.MVVM.Model;

namespace SmartExpense.Core;

public class ApplicationContext : DbContext
{
    private const string TEXT_INCOME_TYPE_DEFINOTION = "Надходження";
    private const string TEXT_OUTCOME_TYPE_DEFINOTION = "Витрата";

    public static UserModel CurrentUser { get; set; } = new UserModel
    {
        Username = "default",
        Password = "default",
    };

    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<TransactionModel> Transactions { get; set; } = null!;
    public DbSet<AccountModel> Accounts { get; set; } = null!;

    public ApplicationContext()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=root_password;database=smartexpensedatabase;",
                new MySqlServerVersion(new Version(8, 0, 33)));
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransactionModel>(entity =>
        {
            entity.ToTable(CurrentUser.Username + "_Transaction");
        });

        modelBuilder.Entity<AccountModel>(entity =>
        {
            entity.ToTable(CurrentUser.Username + "_Transaction");
        });
    }*/

    public static void SetUser(UserModel user)
    {
        CurrentUser = user;
    }

    public UserModel GetUser()
    {
        return CurrentUser;
    }

    public void AddTransaction(TransactionModel transactionModel)
    {
        var updatedAccount = Accounts.Where(account => account.UserId == transactionModel.UserId)
            .FirstOrDefault(account => account.Title == transactionModel.AccountTitle);
        
        if(updatedAccount == null)
            return;
        
        if (transactionModel.Type == TEXT_INCOME_TYPE_DEFINOTION)
        {
            updatedAccount.Amount += transactionModel.Amount;
            Accounts.Update(updatedAccount);
        }
        
        if (transactionModel.Type == TEXT_OUTCOME_TYPE_DEFINOTION)
        {
            updatedAccount.Amount -= transactionModel.Amount;
            Accounts.Update(updatedAccount);
        }
        
        Transactions.Add(transactionModel);
        SaveChanges();
    }

    public void DeleteTransaction(TransactionModel transactionModel)
    {
        var updatedAccount = Accounts.Where(account => account.UserId == transactionModel.UserId)
            .FirstOrDefault(account => account.Title == transactionModel.AccountTitle);
        
        if(updatedAccount == null)
            return;
        
        if (transactionModel.Type == TEXT_INCOME_TYPE_DEFINOTION)
        {
            updatedAccount.Amount -= transactionModel.Amount;
            Accounts.Update(updatedAccount);
        }
        
        if (transactionModel.Type == TEXT_OUTCOME_TYPE_DEFINOTION)
        {
            updatedAccount.Amount += transactionModel.Amount;
            Accounts.Update(updatedAccount);
        }
        
        Transactions.Remove(transactionModel);
        SaveChanges();
    }

    public void AddAccount(AccountModel accountModel)
    {
    }

    public void DeleteAccount(AccountModel accountModel)
    {
    }

    public decimal GetUserBalance() =>
        Accounts.Where(account => account.User.Username == CurrentUser.Username).Sum(account => account.Amount);

    public decimal GetUserIncome() =>
        Transactions.Where(transaction => transaction.User.Username == CurrentUser.Username &&
                                          transaction.Type == TEXT_INCOME_TYPE_DEFINOTION)
            .Sum(transaction => transaction.Amount);

    public decimal GetUserOutcome() =>
        Transactions.Where(transaction => transaction.User.Username == CurrentUser.Username &&
                                          transaction.Type == TEXT_OUTCOME_TYPE_DEFINOTION)
            .Sum(x => x.Amount);

    public ObservableCollection<TransactionModel> GetUserTransactions() =>
        new(Transactions.Where(x => x.User.Username == CurrentUser.Username));

    public ObservableCollection<AccountModel> GetUserAccounts() =>
        new(Accounts.Where(x => x.User.Username == CurrentUser.Username));
}