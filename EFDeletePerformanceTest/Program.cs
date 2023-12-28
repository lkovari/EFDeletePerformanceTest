using EFDeletePerformanceTest.TestCases;

namespace DeletePerformanceTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var deleteExecutor = new DeleteExecutor();

            Console.WriteLine("EntityFramework RemoveRange() vs. ExecuteDelete() performance comparison");
            Console.WriteLine();

            deleteExecutor.ExecuteDelete();

        }
    }
}
