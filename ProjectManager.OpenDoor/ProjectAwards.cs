using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Paint.Data;
using Paint.Domain;

namespace ProjectManager
{
    public class ProjectAward
    {
        private readonly Context context;

//        public List<PriceListLineItem> PriceList { get; set; } = new List<PriceListLineItem>();
//        public BidSheet BidSheet { get; set; } = new BidSheet();

        public ProjectAward(Context context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public void Process(string fileName)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorkbook workbook = package.Workbook;

//                ProcessBidSheet(workbook);
                ProcessPriceList(workbook);
            }
        }

        Job FindCreateJob(string name, string address)
        {
            Client offerpad = context.Clients.Include(c => c.Clients).FirstOrDefault(c => c.CompanyName == "OfferPad");
            string[] parts = name.Split(' ');
            Client pm = context.Clients.Include(c => c.Jobs).FirstOrDefault(c => c.ParentId == offerpad.Id && c.FirstName == parts[0] && c.LastName == parts[1]);
            if (pm == null)
            {
                pm = new Client();
                pm.FirstName = parts[0];
                pm.LastName = parts[1];
                pm.Active = true;
                pm.ParentId = offerpad.Id;
                pm.ClientTypeId = offerpad.ClientTypeId;
                offerpad.Clients.Add(pm);
            }
            string[] addressParts = address.Split(',');
            string street = addressParts.Length >= 1 ? addressParts[0].Trim() : address;
            string city = addressParts?.Length >=2 ? addressParts[1].Trim() : null;
            string state = addressParts?.Length >= 3 ? addressParts[2].Trim().Split(' ')[0] : null;
            string zip = addressParts?.Length >= 3 ? addressParts[2].Trim().Split(' ')[1] : null;

            Job job = pm.Jobs.FirstOrDefault(j => j.Address.Line1 == street && j.Address.City == city && j.Address.State == state && j.Address.ZipCode == zip);
            if (job == null)
            {
                job = new Job();
                job.Address = new Address();
                job.Address.Line1 = street;
                job.Address.City = city;
                job.Address.State = state;
                job.Address.ZipCode = zip;
                pm.Jobs.Add(job);
                int x = context.SaveChanges();
                return job;
            }
            job = context.Jobs.Include(j => j.BidSheet).ThenInclude(bs => bs.Areas).ThenInclude(ba => ba.Items).FirstOrDefault(j => j.Id == job.Id);
            return job;
        }

        public void ProcessBidSheet(ExcelWorkbook workbook)
        {
            ExcelWorksheet worksheet = workbook.Worksheets[0];
            string Address = worksheet.Cells[6, 2].Value as string;
            string ProgjectManager = worksheet.Cells[7, 2].Value as string;

            Job job = FindCreateJob(ProgjectManager, Address);
            if (job.BidSheet == null)
            {
                BidSheet BidSheet = new BidSheet();
                job.BidSheet = BidSheet;

                BidSheet.Date = worksheet.Cells[9, 2].Value is DateTime ? (DateTime)worksheet.Cells[9, 2].Value : DateTime.Parse(worksheet.Cells[9, 2].Value.ToString());
                BidSheet.SquareFoot = worksheet.Cells[10, 2].Value.ToString();
                BidSheet.BedBath = worksheet.Cells[11, 2].Value as string;
                BidSheet.LockBox = worksheet.Cells[12, 2].Value?.ToString();
                BidSheet.Year = worksheet.Cells[13, 2].Value?.ToString();
                BidSheet.RenoTotal = (decimal)((double)worksheet.Cells[11, 6].Value);

                int rowCount = worksheet.Cells.Rows;
                BidArea area = null;
                BidItem item;
                for (int row = 17; row <= rowCount; row++)
                {
                    if ((worksheet.Cells[row, 2] != null && worksheet.Cells[row, 2].Value != null) &&
                        (worksheet.Cells[row, 5].Value == null || worksheet.Cells[row, 5].Value is string))
                    {
                        area = new BidArea();
                        BidSheet.Areas.Add(area);
                        area.Name = worksheet.Cells[row, 2].Value as string;
                        area.Description = worksheet.Cells[row, 3].Value as string;
                    }
                    else if (worksheet.Cells[row, 5].Value != null && worksheet.Cells[row, 5].Value is double)
                    {
                        item = new BidItem();
                        item.Sub = worksheet.Cells[row, 1].Value?.ToString();
                        item.Category = worksheet.Cells[row, 2].Value as string;
                        item.Description = worksheet.Cells[row, 3].Value as string;
                        item.Quantity = (decimal)(((double?)worksheet.Cells[row, 4].Value) ?? 0.0);
                        item.UnitCost = (decimal)((double)worksheet.Cells[row, 5].Value);
                        if (item.Description != null || item.Quantity != 0.0m || item.UnitCost != 0.0m)
                        {
                            area.Items.Add(item);
                        }
                    }
                }

                int x = context.SaveChanges();
            }
        }

        public Category FindCreateCategory(string category, string subCategory)
        {
            Category cat = context.Categories.Include(c => c.SubCategories).FirstOrDefault(c => c.Name == category);
            if (cat == null)
            {
                cat = new Category();
                cat.Name = category;
                context.Categories.Add(cat);
            }
            Category sub = cat.SubCategories.FirstOrDefault(s => s.Name == subCategory);
            if (sub ==  null)
            {
                sub = new Category();
                sub.Name = subCategory;
                cat.SubCategories.Add(sub);
                int x = context.SaveChanges();
            }
            return sub;
        }

        public void ProcessPriceList(ExcelWorkbook workbook)
        {
            ExcelWorksheet worksheet = workbook.Worksheets[3];

            for (int i = 6; i <= worksheet.Cells.Rows; i++)
            {
                if (worksheet.Cells[i, 1] != null && worksheet.Cells[i, 1].Value != null)
                {
                    string category = worksheet.Cells[i, 1].Value as string;
                    string subCategory = worksheet.Cells[i, 2].Value as string;
                    string name = worksheet.Cells[i, 3].Value as string;
                    Category cat = FindCreateCategory(category, subCategory);

                    PriceListLineItem item = context.PriceList.FirstOrDefault(pl => pl.CategoryId == cat.Id && pl.Name == name);
                    if (item == null)
                    {
                        item = new PriceListLineItem();
                        context.PriceList.Add(item);

                        item.CategoryId = cat.Id;
                        item.Name = name;
                        item.MaterialSupplier = worksheet.Cells[i, 4].Value as string;
                        item.ModelNumber = worksheet.Cells[i, 5].Value?.ToString();
                        item.UOM = (UOM)Enum.Parse(typeof(UOM), (worksheet.Cells[i, 6].Value as string));
                        item.MaterialUnitCost = (decimal)(((double?)(worksheet.Cells[i, 7].Value) ?? 0.0));
                        item.Tax = (decimal)((double)worksheet.Cells[i, 8].Value);
                        item.MaterialTotalCost = (decimal)((double)worksheet.Cells[i, 9].Value);
                        item.LaborRate = (decimal)((double)worksheet.Cells[i, 10].Value);
                        item.LaborHours = (decimal)(((double?)(worksheet.Cells[i, 11].Value) ?? 0.0));
                        item.LaborSubtotal = (decimal)((double)worksheet.Cells[i, 12].Value);
                        item.LaborTotal = (decimal)((double)worksheet.Cells[i, 13].Value);
                        item.ProfitMargin = (decimal)((double)worksheet.Cells[i, 14].Value);
                        item.ProfitTotal = (decimal)((double)worksheet.Cells[i, 15].Value);
                        item.TotalCost = (decimal)((double)worksheet.Cells[i, 16].Value);
                    }
                }
            }
        }
    }
}
