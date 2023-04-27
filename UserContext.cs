using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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

            modelBuilder.Entity<UserAuthorization>()
                .HasKey(ua => new { ua.UserId, ua.AuthorizationId });

            modelBuilder.Entity<UserAuthorization>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAuthorizations)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAuthorization>()
                .HasOne(ua => ua.Authorization)
                .WithMany(a => a.UserAuthorizations)
                .HasForeignKey(ua => ua.AuthorizationId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SignupCode> SignupCodes { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Authorization> Authorization { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public ICollection<UserAuthorization> UserAuthorizations { get; set; }
    }

    public class SignupCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTime ExpiresOn { get; set; }
    }

    public class Sessions
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime ExpireOn { get; set; }
        public string SessionCode { get; set; }
    }

    public class Authorization
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public ICollection<UserAuthorization> UserAuthorizations { get; set; }
    }

    public class UserAuthorization
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AuthorizationId { get; set; }
        public Authorization Authorization { get; set; }
    }
}
