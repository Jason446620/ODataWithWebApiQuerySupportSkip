namespace WebAPITest
{
    using Microsoft.OData.Edm;
    using System.Web.Http;
    using System.Web.OData.Batch;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using WebAPITest.Controllers;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();
           

        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "WebAPITest";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<TestData>("TestData");
            builder.EntitySet<CustomerEntity>("CustomerEntity");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
