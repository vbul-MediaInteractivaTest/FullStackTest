using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MediaInteractivaTest.ApiTests.Tests.Employees
{
    public class EmployeeTests
    {
        private long _id;
        private Configuration _configuration;
        public static HttpClient Client = new HttpClient();

        [SetUp]
        public void Setup()
        {
            _configuration = TestsHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);

            var sql = "INSERT INTO [dbo].[Employees]([Name],[Lastname],[IsEmployee]) " +
                      "SELECT N'EmployeeValidationOnUpdate', N'Test', 1 " +
                      "WHERE NOT EXISTS(SELECT NULL FROM [dbo].[Employees] WHERE [Name] = N'EmployeeValidationOnUpdate' AND [Lastname] = N'Test')";
            using SqlConnection connection = new SqlConnection(_configuration.ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

            _id = (long)new SqlCommand(
                "SELECT [Id] FROM [dbo].[Employees] WHERE [Name] = N'EmployeeValidationOnUpdate' AND [Lastname] = N'Test'",
                connection).ExecuteScalar();
        }

        [Test]
        public async Task EmployeeFullLifecycle()
        {
            //POST
            var emp = await Task.Run(() => JsonConvert.SerializeObject(new Employee { Name = TestsHelper.RandomString(20), Lastname = TestsHelper.RandomString(20), IsEmployee = true}));
            HttpResponseMessage response = await Client.PostAsync($"{_configuration.ApiUrl}/api/Employees", new StringContent(emp, Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);

            //PUT
            var responseContent = await response.Content.ReadAsStringAsync();
            var empObj = JsonConvert.DeserializeObject<Employee>(responseContent);
            empObj.IsEmployee = false;

            emp = await Task.Run(() => JsonConvert.SerializeObject(empObj));
            response = await Client.PutAsync($"{_configuration.ApiUrl}/api/Employees/{empObj.Id}", new StringContent(emp, Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);

            //GET
            response = await Client.GetAsync($"{_configuration.ApiUrl}/api/Employees/{empObj.Id}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            responseContent = await response.Content.ReadAsStringAsync();
            empObj = JsonConvert.DeserializeObject<Employee>(responseContent);
            Assert.AreEqual(false, empObj.IsEmployee);

            //DELETE
            response = await Client.DeleteAsync($"{_configuration.ApiUrl}/api/Employees/{empObj.Id}");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestCase(null, "Test")]
        [TestCase("", "Test")]
        [TestCase("Test", null)]
        [TestCase("Test", "")]
        public async Task EmployeeValidationOnCreate(string firstName, string lastName)
        {
            var emp = await Task.Run(() => JsonConvert.SerializeObject(new Employee { Name = firstName, Lastname = lastName, IsEmployee = true }));
            HttpResponseMessage response = await Client.PostAsync($"{_configuration.ApiUrl}/api/Employees", new StringContent(emp, Encoding.UTF8, "application/json"));
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestCase(null, "Test")]
        [TestCase("", "Test")]
        [TestCase("Test", null)]
        [TestCase("Test", "")]
        public async Task EmployeeValidationOnUpdate(string firstName, string lastName)
        {
            var emp = await Task.Run(() => JsonConvert.SerializeObject(new Employee { Id = _id, Name = firstName, Lastname = lastName, IsEmployee = true }));
            HttpResponseMessage response = await Client.PutAsync($"{_configuration.ApiUrl}/api/Employees/{_id}", new StringContent(emp, Encoding.UTF8, "application/json"));
            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}