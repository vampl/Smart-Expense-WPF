using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SmartExpense.MVVM.Model;

namespace SmartExpense.Core;

public class ApplicationContext : DbContext
{
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
}