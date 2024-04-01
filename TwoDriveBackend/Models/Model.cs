using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TwoDrive.Models;

public class TwoDriveContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Drive> Drive { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<Token> Token { get; set; }

    public string DbPath { get; }

    public TwoDriveContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "twodrive.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Token>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId);
    }
}
