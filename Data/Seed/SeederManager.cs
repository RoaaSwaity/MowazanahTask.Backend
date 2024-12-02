using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace Data.Seed
{
    public class SeederManager
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (!context.Stocks.Any())
            {
                var data = GetStockData();

                context.Stocks.AddRange(data);
                context.SaveChanges();
            }
        }


        public void Seed(ref ModelBuilder modelBuilder)
        {
            var data = GetStockData();

            foreach (var item in data)
            {
                modelBuilder.Entity<Stock>().HasData(
                    new Stock
                    {
                        MidPrice = item.MidPrice,
                        SymbolName = item.SymbolName,
                        Time = item.Time,
                    }
                    );
            }

        }

        static public List<Stock> GetStockData()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string? projectRoot = Directory.GetParent(baseDirectory)?.Parent?.Parent?.FullName;
            var fullPath = Path.Combine(projectRoot.Replace("bin", ""), "Data", "stock_data.csv");

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"The file at path stock_data.csv does not exist.");

            var stocks = new List<Stock>();
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader, config))
            {                
                csv.Read();
                csv.ReadHeader();
                
                while (csv.Read())
                {                    
                    var time = csv.GetField<string>("Timestamp");
                    var symbolName = csv.GetField<string>("SymbolName");
                    var midPrice = csv.GetField<decimal>("MidPrice");

                    stocks.Add(new Stock
                    {
                        Time = time,
                        MidPrice = midPrice,
                        SymbolName = symbolName ?? ""
                    });
                }

            }

            return stocks;
        }
    }
}
