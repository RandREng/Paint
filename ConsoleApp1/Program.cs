using Microsoft.EntityFrameworkCore;
using Paint.Data;
using Paint.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context ctx = new Context())
            {
                ctx.Database.EnsureCreated();
                ctx.ChangeTracker.Tracked += ChangeTracker_Tracked;
                ctx.ChangeTracker.StateChanged += ChangeTracker_StateChanged;

                Client client = ctx.Clients.FirstOrDefault();
                Client client2 = new Client();
                client2.FirstName = "Chase";
                client2.Active = true;
                client2.ClientTypeId = client.ClientTypeId;
                client.Clients.Add(client2);
                Job job1 = new Job();
                client2.Jobs.Add(job1);
                //Job job2 = ctx.Jobs.FirstOrDefault();
                job1.PaintList = new PaintList();                //client.Jobs.Add(job1);
                ctx.ChangeTracker.DetectChanges();
                int x = ctx.SaveChanges();
            }
            using (Context ctx = new Context())
            {
                ctx.ChangeTracker.Tracked += ChangeTracker_Tracked;
                ctx.ChangeTracker.StateChanged += ChangeTracker_StateChanged;

                List<Client> client = ctx.Clients.Where(c => c.ParentId == null).Include(c => c.Clients).ToList();
            }
        }

        private static void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            System.Console.WriteLine($"StateChanged - {e.Entry.Entity.GetType().Name}");
        }

        private static void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            System.Console.WriteLine($"Tracked - {e.Entry.Entity.GetType().Name}");
        }
    }
}
