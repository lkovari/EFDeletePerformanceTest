using DomainModel;
using System.Text.Json;
using static DataModel.DataModel;

namespace EFDeletePerformanceTest.TestCases
{
    internal class DeleteExecutorBase
    {
        private readonly string _jsonFilePath = "\\net8.0\\ZipCodes.json";

        protected List<ZipCode> _zipCodesToInsert;

        public DeleteExecutorBase()
        {
            _jsonFilePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + _jsonFilePath;
            DataContext _dataContext = new();
            _dataContext.Database.EnsureCreated();
            _zipCodesToInsert = LoadData();
        }


        public int PrepareDataToDelete()
        {
            var dataContext = new DataContext();
            dataContext.ZipCode.AddRange(_zipCodesToInsert);
            return dataContext.SaveChanges();
        }

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

        public List<ZipCode> GetZipCodeEntities()
        {
            var dataContext = new DataContext();
            return [.. dataContext.ZipCode];
        }
    }
}
