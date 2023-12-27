using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static DataModel.DataModel;

namespace DeletePerformanceTest
{
    internal class Program
    {
        private const string JSON_FILE_PATH
            = "C:\\src\\C#\\EFDeletePerformanceTest\\DataModel\\ZipCodes.json";

        static void Main(string[] args)
        {
            Console.WriteLine("EntityFramework RemoveRange() vs. ExecuteDelete() performance comparison");
            Console.WriteLine();

            var zipCodes = GetZipCodes();
            if (zipCodes != null && zipCodes.Count < 1)
            {
                List<ZipCode> zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject(JSON_FILE_PATH);
                using var context = new DataContext();
                context.ZipCode.AddRange(zipCodesToInsert);
                context.SaveChanges();
            }
            zipCodes = GetZipCodes();
            Console.WriteLine("Initial count of US. Zip Codes {0}.", zipCodes.Count);

            DeleteZipCodesWithExecuteDelete();

            zipCodes = GetZipCodes();
            if (zipCodes != null && zipCodes.Count < 1)
            {
                List<ZipCode> zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject(JSON_FILE_PATH);
                using var context = new DataContext();
                context.ZipCode.AddRange(zipCodesToInsert);
                context.SaveChanges();
            }

            DeleteZipCodesWithRemoveRange();

            zipCodes = GetZipCodes();
            if (zipCodes != null && zipCodes.Count < 1)
            {
                List<ZipCode> zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject("C:\\src\\C#\\LKEFPlayground\\ZipCodeData\\ZipCodes.json");
                using var context = new DataContext();
                context.ZipCode.AddRange(zipCodesToInsert);
                context.SaveChanges();
            }
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var zipCodes = GetZipCodes();
            using var context = new DataContext();
            context.ZipCode.RemoveRange(zipCodes);
            context.SaveChanges();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.WriteLine("Delete ZipCodes with RemoveRange elapsed time {0}ms.", elapsedMils);
        }

        private static void DeleteZipCodesWithExecuteDelete()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using var context = new DataContext();
            context.ZipCode.Where(t => t.Id > 0).ExecuteDelete();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.WriteLine("Delete ZipCodes with ExecuteDelete elapsed time {0}ms.", elapsedMils);
        }
    }
}
