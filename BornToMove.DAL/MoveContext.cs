﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        public DbSet<Move> Move { get; set; }
        public DbSet<MoveRating> MoveRating { get; set; }

        public static readonly LoggerFactory _myLoggerFactory =
        new LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        });

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=born2move;Trusted_Connection=True;");
            builder.UseLoggerFactory(_myLoggerFactory);
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Move>().HasData(
                new Move
                {
                    Id = 1,
                    Name = "Push up",
                    Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes.",
                    SweatRate = 3
                },
                new Move
                {
                    Id = 2,
                    Name = "Planking",
                    Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast.",
                    SweatRate = 3
                },
                new Move
                {
                    Id = 3,
                    Name = "Squat",
                    Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes.",
                    SweatRate = 5
                });
        }

    }
}