using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Paint.Domain;
using RandREng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Data
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<PaintItem> Paints { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PriceListLineItem> PriceList { get; set; }
        public DbSet<BidSheet> BidSheets { get; set; }
        public DbSet<BidItem> BidItems { get; set; }

        public Context()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public Context(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureSqlServer<Context>("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=NRSDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaintList>(
                entity =>
                {
                    entity.HasKey(p => p.JobId);
                    entity.HasOne(p => p.Job)
                    .WithOne(j => j.PaintList)
                    .HasForeignKey<PaintList>(p => p.JobId);
                });

            //modelBuilder.Entity<Job>()
            //    .HasOne(j => j.PaintList)
            //    .WithOne(p => p.Job)
            //    .HasForeignKey<PaintList>(p => p.JobId);

            modelBuilder.Entity<ClientType>(entity
                =>
            {
                entity.HasData(
                    new ClientType
                    {
                        Id = 1,
                        Name = "REO"
                    },
                    new ClientType
                    {
                        Id = 2,
                        Name = "Comercial"
                    },
                    new ClientType
                    {
                        Id = 3,
                        Name = "Residential"
                    }
                ); ;
            });

            modelBuilder.Entity<Client>(entity
                =>
            {
                entity.HasData(
                    new Client
                    {
                        Id = 1,
                        CompanyName = "OfferPad",
                        ClientTypeId = 1,
                        Active = true
                    },
                    new Client
                    {
                        Id = 2,
                        CompanyName = "OfferPad",
                        FirstName = "Chase",
                        LastName = "Timms",
                        ClientTypeId = 1,
                        ParentId = 1,
                        Active = true
                    }
                );
            });
            modelBuilder.Entity<PaintItem>(entity =>
            {
                entity.HasData(
                    new PaintItem
                    {
                        Id = 1,
                        Type = PaintType.Ceiling,
                        Grade = PaintGrade.Good,
                        Name = "SPEED-WALL Flat",
                        LowCoverage = 300,
                        HiCoverage = 400,
                        GallonPrice = 11.98M,
                        FiveGallonPrice = 44.98M,
                    },
                new PaintItem
                {
                    Id = 2,
                    Type = PaintType.Ceiling,
                    Grade = PaintGrade.Better,
                    Name = "PPG ULTRA-HIDE Zero Flat",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 19.98M,
                    FiveGallonPrice = 70.98M,
                },
                new PaintItem
                {
                    Id = 3,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Good,
                    Name = "GLIDDEN ESSENTIALS SG",
                    LowCoverage = 300,
                    HiCoverage = 400,
                    GallonPrice = 21.98M,
                    FiveGallonPrice = 0M,
                },
                new PaintItem
                {
                    Id = 4,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Better,
                    Name = "PPG Ultra-Hide Zero SG",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 19.98M,
                    FiveGallonPrice = 89.98M,
                },
                new PaintItem
                {
                    Id = 5,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Best,
                    Name = "Glidden Premium SG",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 22.98M,
                    FiveGallonPrice = 102M,
                },
                new PaintItem
                {
                    Id = 6,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Premium,
                    Name = "PPG DIAMOND Eggshell",
                    LowCoverage = 300,
                    HiCoverage = 400,
                    GallonPrice = 25.98M,
                    FiveGallonPrice = 121M,
                },
                new PaintItem
                {
                    Id = 7,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Premium,
                    Name = "PPG TIMELESS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 36.98M,
                    FiveGallonPrice = 168M,
                },
                new PaintItem
                {
                    Id = 8,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Good,
                    Name = "GLIDDEN ESSENTIALS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 14.98M,
                    FiveGallonPrice = 76.98M,
                },
                new PaintItem
                {
                    Id = 9,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Better,
                    Name = "PPG Ultra-Hide Zero Eggshell",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 17.98M,
                    FiveGallonPrice = 79.98M,
                },
                new PaintItem
                {
                    Id = 10,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Best,
                    Name = "Glidden Premium Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 20.98M,
                    FiveGallonPrice = 97.98M,
                },
                new PaintItem
                {
                    Id = 11,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Premium,
                    Name = "PPG DIAMOND Eggshell",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 25.98M,
                    FiveGallonPrice = 112M,
                },
                new PaintItem
                {
                    Id = 12,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Premium,
                    Name = "PPG TIMELESS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 34.98M,
                    FiveGallonPrice = 159M,
                });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
