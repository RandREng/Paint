using Paint.Data;
using Paint.Domain;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context ctx = new Context())
            {

                Client client = ctx.Clients.FirstOrDefault();
                Client client2 = new Client();
                client2.FirstName = "Chase";
                client2.Active = true;
                client2.ClientTypeId = client.ClientTypeId;
                client.Clients.Add(client2);
                //Job job1 = new Job();
                //Job job2 = ctx.Jobs.FirstOrDefault();
                //job2.PaintList = new PaintList();                //client.Jobs.Add(job1);
                int x = ctx.SaveChanges();
            }
        }
    }
}
