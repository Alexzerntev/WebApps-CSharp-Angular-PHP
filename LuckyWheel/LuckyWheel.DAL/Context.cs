using System;
using System.Collections.Generic;
using System.Text;
using LuckyWheel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LuckyWheel.DAL
{
    public class Context : IdentityDbContext<User, Role, Guid>

    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<WheelSegment> WheelSegments { get; set; }
        public DbSet<Spin> Spins { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<DepositCode> DepositCodes { get; set; }
        public DbSet<WheelSetting> WheelSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Lw");

            //Models
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<WheelSegment>().ToTable("WheelSegments");
            modelBuilder.Entity<Spin>().ToTable("Spins");
            modelBuilder.Entity<Photo>().ToTable("Photos");
            modelBuilder.Entity<DepositCode>().ToTable("DepositCodes");
            modelBuilder.Entity<WheelSetting>().ToTable("WheelSettings");
        }
    }
}
