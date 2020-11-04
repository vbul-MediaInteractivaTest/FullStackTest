using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MediaInteractivaTest.ApiTests.Tests.Pets
{
    public class PetTests
    {
        private long _id;
        private long _ownerId;
        private Configuration _configuration;
        public static HttpClient Client = new HttpClient();

        [SetUp]
        public void Setup()
        {
            _configuration = TestsHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);

            var sql = "INSERT INTO [dbo].[Employees]([Name],[Lastname],[IsEmployee]) " +
                      "SELECT N'PetFullLifecycle', N'Test', 1 " +
                      "WHERE NOT EXISTS(SELECT NULL FROM [dbo].[Employees] WHERE [Name] = N'PetFullLifecycle' AND [Lastname] = N'Test')";
            using SqlConnection connection = new SqlConnection(_configuration.ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

            _ownerId = (long)new SqlCommand(
                "SELECT [Id] FROM [dbo].[Employees] WHERE [Name] = N'PetFullLifecycle' AND [Lastname] = N'Test'",
                connection).ExecuteScalar();

            sql = "INSERT INTO [dbo].[Pets]([Name],[Type],[OwnerId]) " +
                  $"SELECT N'PetValidationOnUpdate', 1, {_ownerId} " +
                  "WHERE NOT EXISTS(SELECT NULL FROM [dbo].[Pets] WHERE [Name] = N'PetValidationOnUpdate')";
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            _id = (long)new SqlCommand(
                "SELECT [Id] FROM [dbo].[Pets] WHERE [Name] = N'PetValidationOnUpdate'",
                connection).ExecuteScalar();
        }

        [Test]
        public async Task PetFullLifecycle()
        {
            //POST
            var pet = await Task.Run(() => JsonConvert.SerializeObject(new Pet { Type = 1, Name = TestsHelper.RandomString(20), OwnerId = _ownerId }));
            HttpResponseMessage response = await Client.PostAsync($"{_configuration.ApiUrl}/api/Pets", new StringContent(pet, Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);

            //PUT
            var responseContent = await response.Content.ReadAsStringAsync();
            var petObj = JsonConvert.DeserializeObject<Pet>(responseContent);
            petObj.Type = 2;

            pet = await Task.Run(() => JsonConvert.SerializeObject(petObj));
            response = await Client.PutAsync($"{_configuration.ApiUrl}/api/Pets/{petObj.Id}", new StringContent(pet, Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);

            //GET
            response = await Client.GetAsync($"{_configuration.ApiUrl}/api/Pets/{petObj.Id}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            responseContent = await response.Content.ReadAsStringAsync();
            petObj = JsonConvert.DeserializeObject<Pet>(responseContent);
            Assert.AreEqual(2, petObj.Type);

            //DELETE
            response = await Client.DeleteAsync($"{_configuration.ApiUrl}/api/Pets/{petObj.Id}");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task PetValidationOnCreate(string name)
        {
            var pet = await Task.Run(() => JsonConvert.SerializeObject(new Pet { Name = name, Type = 1, OwnerId = _ownerId}));
            HttpResponseMessage response = await Client.PostAsync($"{_configuration.ApiUrl}/api/Pets", new StringContent(pet, Encoding.UTF8, "application/json"));
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task PetValidationOnUpdate(string name)
        {
            var emp = await Task.Run(() => JsonConvert.SerializeObject(new Pet { Id = _id, Name = name, Type = 1, OwnerId = _ownerId}));
            HttpResponseMessage response = await Client.PutAsync($"{_configuration.ApiUrl}/api/Pets/{_id}", new StringContent(emp, Encoding.UTF8, "application/json"));
            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}
