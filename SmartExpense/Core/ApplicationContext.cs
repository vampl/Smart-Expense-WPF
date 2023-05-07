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

    private UserModel CurrentUser { get; set; }

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

    public decimal GetUserBalance(string username) =>
        Accounts.Where(account => account.User.Username == username).Sum(account => account.Amount);

    public decimal GetUserIncome(string username) =>
        Transactions.Where(transaction => transaction.User.Username == username && transaction.Type == TEXT_INCOME_TYPE_DEFINOTION)
            .Sum(transaction => transaction.Amount);

    public decimal GetUserOutcome(string username) =>
        Transactions.Where(transaction => transaction.User.Username == username && transaction.Type == TEXT_OUTCOME_TYPE_DEFINOTION)
            .Sum(x => x.Amount);

    public ObservableCollection<TransactionModel> GetUserTransactions(string username) =>
        new(Transactions.Where(x => x.User.Username == username));

    public ObservableCollection<AccountModel> GetUserAccounts(string username) =>
        new(Accounts.Where(x => x.User.Username == username));
}