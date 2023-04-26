﻿using Microsoft.EntityFrameworkCore;

namespace SessionFinalProject
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SignupCode> SignupCodes { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Authorzation> Authorzation { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }

    public class SignupCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTime ExpiersOn { get; set; }
    }

    public class Sessions
    {
        public int id { get; set; }
        public User user { get; set; }
        public DateTime ExpireOn { get; set; }
        public string sessionCode {get; set;}
    }

    public class Authorzation
    {
        
        public int id { get; set; }
        public string role { get; set; }

    }

}