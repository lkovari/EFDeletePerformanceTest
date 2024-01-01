using DataModel;
using DomainModel;
using static DataModel.DataModel;

namespace EFDeletePerformanceTest.TestCases
{
    internal class DeleteExecutorBase
    {
        protected List<ZipCode> _zipCodesToInsert;

        public DeleteExecutorBase()
        {
            DataContext dataContext = new();
            dataContext.Database.EnsureCreated();
            _zipCodesToInsert = PrepareMigrationsDataHelper.ProvideSeedData();
        }


        public int PrepareDataToDelete()
        {
            var dataContext = new DataContext();
            dataContext.ZipCode.AddRange(_zipCodesToInsert);
            return dataContext.SaveChanges();
        }
        /*
        public List<ZipCode> LoadData()
        {
            if (_zipCodesToInsert == null || _zipCodesToInsert.Count < 1)
            {
                _zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject(_jsonFilePath);
            }

            return _zipCodesToInsert;
        }

        private List<ZipCode> DeserializeZipCodeJson2ZipCodeObject(string filePathAndName)
        {
            var zipCodesJson = File.ReadAllText(filePathAndName);
            return JsonSerializer.Deserialize<List<ZipCode>>(zipCodesJson,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }
        */
        public List<ZipCode> GetZipCodeEntities()
        {
            var dataContext = new DataContext();
            return [.. dataContext.ZipCode];
        }
    }
}
