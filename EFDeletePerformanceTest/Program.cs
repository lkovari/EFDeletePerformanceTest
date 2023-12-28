using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static DataModel.DataModel;

namespace DeletePerformanceTest
{
    internal class Program
    {
        private static string JsonFilePath = "\\net8.0\\ZipCodes.json";

        private static List<ZipCode> _zipCodesToInsert = new List<ZipCode>();

        static void Main(string[] args)
        {
            JsonFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + JsonFilePath;
            Console.WriteLine("EntityFramework RemoveRange() vs. ExecuteDelete() performance comparison");
            Console.WriteLine();

            var count = GetZipCodes().Count;
            if (count < 1)
            {
                count = SaveZipCodes();
            }
            Console.WriteLine("Initial count of US. Zip Codes before execute ExecuteDelete {0}.", count);

            DeleteZipCodesWithExecuteDelete();

            count = GetZipCodes().Count;
            Console.WriteLine("Zip Codes deleted with ExecuteDelete, remains #{0} rows.", count);

            if (count < 1)
            {
                count = SaveZipCodes();
            }
            Console.WriteLine("Initial count of US. Zip Codes before execute RemoveRange {0}.", count);

            DeleteZipCodesWithRemoveRange();

            count = GetZipCodes().Count;
            Console.WriteLine("Zip Codes deleted with RemoveRange, remains #{0} rows.", count);
            if (count < 1)
            {
                count = SaveZipCodes();
            }
            Console.WriteLine("#{0} Zip Codes restored into Db.", count);
        }

        private static int SaveZipCodes()
        {
            var zipCodesToInsert = LoadZipCodes();
            using var context = new DataContext();
            context.ZipCode.AddRange(zipCodesToInsert);
            return context.SaveChanges();
        }

        private static List<ZipCode> LoadZipCodes()
        {
            if (_zipCodesToInsert.Count < 1)
            {
                _zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject(JsonFilePath);
            }

            return _zipCodesToInsert;
        }

        private static List<ZipCode> DeserializeZipCodeJson2ZipCodeObject(string filePathAndName)
        {
            var zipCodesJson = File.ReadAllText(filePathAndName);
            return JsonSerializer.Deserialize<List<ZipCode>>(zipCodesJson,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        private static List<ZipCode> GetZipCodes()
        {
            using var context = new DataContext();
            return [.. context.ZipCode];
        }


        private static void DeleteZipCodesWithRemoveRange()
        {
            var zipCodes = GetZipCodes();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using var context = new DataContext();
            context.ZipCode.RemoveRange(zipCodes);
            var deletedRowsCount = context.SaveChanges();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            zipCodes = GetZipCodes();
            Console.Write("Delete #{0} ZipCodes with RemoveRange elapsed time {1}ms.", deletedRowsCount, elapsedMils);
            Console.WriteLine(zipCodes.Count > 0 ? " Failed" : " Success");
        }

        private static void DeleteZipCodesWithExecuteDelete()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using var context = new DataContext();
            var deletedRowsCount = context.ZipCode.Where(t => t.Id > 0).ExecuteDelete();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.Write("Delete #{0} ZipCodes with ExecuteDelete elapsed time {1}ms.", deletedRowsCount, elapsedMils);
            var zipCodes = GetZipCodes();
            Console.WriteLine(zipCodes.Count > 0 ? " Failed" : " Success");

        }
    }
}
