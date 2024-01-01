using DomainModel;
using System.Text.Json;
using static DataModel.DataModel;

namespace DataModel
{
    public class PrepareMigrationsDataHelper
    {
        private static string _jsonFilePath = "\\net8.0\\ZipCodes.json";

        private static List<ZipCode> _zipCodesToInsert;



        public static List<ZipCode> ProvideSeedData()
        {
            _jsonFilePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + _jsonFilePath;
            DataContext _dataContext = new();
            _dataContext.Database.EnsureCreated();
            _zipCodesToInsert = LoadData();
            return _zipCodesToInsert;
        }

        public static List<ZipCode> LoadData()
        {
            if (_zipCodesToInsert == null || _zipCodesToInsert.Count < 1)
            {
                _zipCodesToInsert = DeserializeZipCodeJson2ZipCodeObject(_jsonFilePath);
            }

            return _zipCodesToInsert;
        }

        private static List<ZipCode> DeserializeZipCodeJson2ZipCodeObject(string filePathAndName)
        {
            var zipCodesJson = File.ReadAllText(filePathAndName);
            return JsonSerializer.Deserialize<List<ZipCode>>(zipCodesJson,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }
    }
}
