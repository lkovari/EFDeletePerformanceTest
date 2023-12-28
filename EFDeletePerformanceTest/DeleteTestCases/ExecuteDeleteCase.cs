using EFDeletePerformanceTest.TestCaseInterface;
using Microsoft.EntityFrameworkCore;
using static DataModel.DataModel;

namespace EFDeletePerformanceTest.DeleteTestCases
{
    public class ExecuteDeleteCase() : IDeleteCase
    {
        private readonly DataContext _DbContext = new();
        public readonly string _testCaseName = "ExecuteDelete";

        public int Delete()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int deletedRowsCount = _DbContext.ZipCode.Where(t => t.Id > 0).ExecuteDelete();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.Write("Delete #{0} ZipCodes with {1}, elapsed time is {2}ms.", deletedRowsCount, _testCaseName, elapsedMils);
            return deletedRowsCount;
        }
    }
}
