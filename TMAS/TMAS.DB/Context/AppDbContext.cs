using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMAS.DB.Models;

namespace TMAS.DB.Context
{
   public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<History> Histories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<History>()
                .Property(e => e.ActionType)
                .HasConversion<string>();

            modelBuilder.Entity<Board>()
                .Property(u => u.Title)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<Column>()
               .Property(u => u.Title)
               .HasColumnType("varchar(100)");

            modelBuilder.Entity<User>()
               .Property(u => u.Name)
               .HasColumnType("varchar(30)");

            modelBuilder.Entity<User>()
               .Property(u => u.Password)
               .HasColumnType("varchar(50)");

            modelBuilder.Entity<User>()
               .Property(u => u.Lastname)
               .HasColumnType("varchar(30)");

            modelBuilder.Entity<Card>()
                .Property(u => u.Text)
                .HasColumnType("varchar(5000)");

            modelBuilder.Entity<Board>()
                .Property(b => b.Title)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(b => b.Text)
                .IsRequired();

            modelBuilder.Entity<Column>()
                .Property(b => b.Title)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .HasOne(p => p.User)
                .WithMany(b => b.Boards)
                .HasForeignKey(b => b.BoardUserId);

            modelBuilder.Entity<Column>()
                .HasOne(p => p.Board)
                .WithMany(b => b.Columns);

            modelBuilder.Entity<Card>()
                .HasOne(p => p.Column)
                .WithMany(b => b.Cards);

            modelBuilder.Entity<History>()
                .HasOne(p => p.User)
                .WithMany(b => b.Histories)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
