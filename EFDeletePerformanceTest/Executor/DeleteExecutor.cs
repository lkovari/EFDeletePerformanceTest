using EFDeletePerformanceTest.DeleteTestCases;
using EFDeletePerformanceTest.TestCaseInterface;

namespace EFDeletePerformanceTest.TestCases
{
    internal class DeleteExecutor : DeleteExecutorBase
    {
        private readonly List<IDeleteCase> _deleteCases;

        public DeleteExecutor() : base()
        {
            _deleteCases = [new ExecuteDeleteCase(), new RemoveRangeCase(_zipCodesToInsert), new RawSqlCase()];

        }
        public void ExecuteDelete()
        {
            var count = 0;
            foreach (IDeleteCase deleteCase in _deleteCases)
            {
                var zipCodes = GetZipCodeEntities();
                count = zipCodes.Count;
                if (count < 1)
                {
                    count = PrepareDataToDelete();
                }
                Console.WriteLine("Initial count of US. Zip Codes before delete #{0} rows.", count);

                deleteCase.DeleteEntities();

                zipCodes = GetZipCodeEntities();
                count = zipCodes.Count;
                Console.WriteLine(zipCodes.Count > 0 ? " Failed" : " Success");

                Console.WriteLine("Zip Codes deleted, remains #{0} rows.", count);
            }
            if (count < 1)
            {
                count = PrepareDataToDelete();
            }
            Console.WriteLine("Zip Codes inserted into Db. table #{0} rows.", count);
        }
    }
}
