using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trello.Data.Entities;

namespace Trello.Data
{
    public class ApplicationDbContext : IdentityDbContext <User>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Column> Column { get; set; }
        public DbSet<Card> Cards { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Board>().HasMany<Departament>(board => board.Departaments).WithMany(departament => departament.Boards).UsingEntity(join => join.ToTable("BoardDepartaments"));
            builder.Entity<Board>().HasOne<User>(c => c.Autor).WithMany(user => user.Boards);
           
            builder.Entity<Column>().HasOne<User>(c => c.Autor).WithMany(user=>user.Columns);
            builder.Entity<Column>().HasOne<Board>(c => c.Board).WithMany(board =>board.Columns);

            builder.Entity<Card>().HasOne<Column>(c => c.Column).WithMany(colums => colums.Cards);
            builder.Entity<Card>().HasOne<User> (c => c.Autor).WithMany(user => user.Cards);
            builder.Entity<Card>().HasMany<User>(c => c.Teams).WithMany(user => user.CardTeams).UsingEntity(join => join.ToTable("CardTeams"));

            builder.Entity<Comment>().HasOne<User>(c => c.Autor).WithMany(user => user.Comments);
            builder.Entity<Comment>().HasOne<Card>(c => c.Card).WithMany(card => card.Comments);

            //builder.Entity<Departament>().HasOne<User>(c => c.Boss).WithOne(boss => boss.MyDep).HasForeignKey<Departament>(key=>key.BossId);
           // builder.Entity<Departament>().HasMany<User>(c => c.Teams).WithOne(teams => teams.Departament).HasForeignKey(key=>key.DepartamentId);


        }
    }
}
