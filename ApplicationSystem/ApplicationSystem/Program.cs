using ApplicationSystem.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSystem
{
    class Program
    {
        // Create a new instance of CosmosClient using the emulator's credentials.
        public static readonly CosmosClient client = new(
            accountEndpoint: "https://localhost:8081/",
            authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
        );

        // The database we will create
        static Database database;


        /// <summary>
        /// Entry point to call methods that operate on Azure Cosmos DB resources
        /// </summary>
        public static async Task GetStartedDemo()
        {
            await CreateDatabase();
            await CreateContainers();
        }

        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        private static async Task CreateDatabase()
        {
            // Create a new database.
            database = await client.CreateDatabaseIfNotExistsAsync(
                id: "application"
            );
        }

        /// <summary>
        /// Create the container if it does not exist. 
        /// </summary>
        /// <returns></returns>
        private static async Task CreateContainers()
        {
            // Create a new container.
            await database.CreateContainerIfNotExistsAsync(
                id: "program",
                partitionKeyPath: "/Title"
            );

            await database.CreateContainerIfNotExistsAsync(
                id: "applicationForm",
                partitionKeyPath: "/Id"
            );

            await database.CreateContainerIfNotExistsAsync(
                id: "workflow",
                partitionKeyPath: "/Id"
            );
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ProgramService>();
            services.AddScoped<ApplicationFormService>();
            services.AddScoped<WorkflowService>();
        }


        public static async Task Main(string[] args)
        {
            // Create Database and Containers.
            await GetStartedDemo();

            // Implementing Dependency Injection.
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve Program Service.
            //var programService = serviceProvider.GetService<ProgramService>();
            //await programService.ReplaceItem();


        }
    }
}