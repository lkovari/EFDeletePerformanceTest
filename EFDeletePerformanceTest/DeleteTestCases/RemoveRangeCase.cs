using DomainModel;
using EFDeletePerformanceTest.TestCaseInterface;
using static DataModel.DataModel;

namespace EFDeletePerformanceTest.DeleteTestCases
{
    public class RemoveRangeCase(List<ZipCode> zipCodes) : IDeleteCase
    {
        private readonly DataContext _DbContext = new();
        private readonly List<ZipCode> _zipCodes = zipCodes;

        public readonly string _testCaseName = "RemoveRange";

        public int Delete()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _DbContext.ZipCode.RemoveRange(_zipCodes);
            var deletedRowsCount = _DbContext.SaveChanges();
            watch.Stop();
            var elapsedMils = watch.ElapsedMilliseconds;
            Console.Write("Delete #{0} ZipCodes with {1}, elapsed time is {2}ms.", deletedRowsCount, _testCaseName, elapsedMils);
            return deletedRowsCount;
        }
    }
}
