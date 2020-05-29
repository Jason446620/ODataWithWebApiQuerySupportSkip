
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace WebAPITest.Controllers
{
    public class TestDataController : ODataController
    {
        [EnableQuery]
        public IHttpActionResult Get()
        {
            var result = CreateTestData().AsQueryable();
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsPr***ix=core.windows.net");
            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("test");
            IQueryable<CustomerEntity> linqQuery = table.CreateQuery<CustomerEntity>().Where(x => x.PartitionKey != "0")
            .Select(x => new CustomerEntity() { PartitionKey = x.PartitionKey, RowKey = x.RowKey, Name = x.Name, Role = x.Role });
            var a = linqQuery.ToList<CustomerEntity>().AsQueryable();
            return Ok(a);
        }

        public List<TestData> CreateTestData()
        {
            List<TestData> data = new List<TestData>();
            data.Add(new TestData { Id = 1, Name = "Jignesh", Role = "Project Manager" });
            data.Add(new TestData { Id = 2, Name = "Tejas", Role = "Architect" });
            data.Add(new TestData { Id = 3, Name = "Rakesh", Role = "Lead" });

            return data;
        }
    }
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity()
        {
        }

        public CustomerEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public int ID { get; set; }
    }
}
