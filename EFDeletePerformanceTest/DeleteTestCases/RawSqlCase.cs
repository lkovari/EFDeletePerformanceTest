using EFDeletePerformanceTest.TestCaseInterface;
using Microsoft.EntityFrameworkCore;
using static DataModel.DataModel;

namespace EFDeletePerformanceTest.DeleteTestCases
{
    public class RawSqlCase : IDeleteCase
    {
        private readonly DataContext _DbContext = new();
        public readonly string _testCaseName = "Raw Sql";
        public int DeleteEntities()
        {
            string sqlDeleteStatement = "DELETE FROM ZipCode";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int deletedRowsCount = _DbContext.Database.ExecuteSqlRaw(sqlDeleteStatement);
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.Write("Delete #{0} ZipCodes with {1}, elapsed time is {2}ms.", deletedRowsCount, _testCaseName, elapsedMils);
            return deletedRowsCount;
        }
    }
}
