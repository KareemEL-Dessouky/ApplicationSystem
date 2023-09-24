using ApplicationSystem.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace ApplicationSystem.Services
{
    // Tab 1
    public class ProgramService
    {
        private readonly Database database;
        private readonly Container container;

        public ProgramService()
        {
            database = Program.client.GetDatabase("application");
            container = database.GetContainer("program");
        }

        // Post.
        public async Task AddItemsToContainer()
        {
            ProgramDetails programDetails = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Internship program",
                Description = "A program that targets thrilled Software Developers with knowledge of Cloud",
                RequiredSkills = new() { "C#", "Entity Framework Core", "Familiarity with Cloud services" },
                AdditionalInfo = new()
                {
                    ApplicationMaxNumber = 100,
                    ApplicationOpen = new(2023, 10, 1),
                    ApplicationClose = new(2023, 4, 30),
                    Location = "Fully Remote",
                    Type = "Full Time"
                }
            };

            try
            {
                // Read the item to see if it exists.  
                ItemResponse<ProgramDetails> programDetailsResponse = await             container.ReadItemAsync<ProgramDetails>(programDetails.Id, new PartitionKey(programDetails.Title));
                Console.WriteLine("Item in database with id: {0} already exists\n", programDetailsResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<ProgramDetails> programDetailsResponse = await container.CreateItemAsync(programDetails, new PartitionKey(programDetails.Title));

                Console.WriteLine("Created item in database with id: {0} \n", programDetailsResponse.Resource.Id);
            }
        }


        // Get for Tab1 and Tab 4.
        public async Task GetItem()
        {
            var sqlQueryText = "SELECT * FROM p WHERE p.Title = 'Internship program'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new(sqlQueryText);
            FeedIterator<ProgramDetails> queryResultSetIterator = container.GetItemQueryIterator<ProgramDetails>(queryDefinition);

            List<ProgramDetails> programs = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ProgramDetails> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (ProgramDetails program in currentResultSet)
                {
                    programs.Add(program);
                    Console.WriteLine("\tRead {0}\n", program);
                }
            }
        }


        // Put.
        public async Task ReplaceItem()
        {
            ItemResponse<ProgramDetails> programDetailsResponse = await container.ReadItemAsync<ProgramDetails>("d58deb83-99ae-4b0e-bc7c-b633a7a78dbe", new PartitionKey("Internship program"));

            var itemBody = programDetailsResponse.Resource;

            // update Summary Property
            itemBody.Summary = "Test Summary";

            // replace the item with the updated content
            programDetailsResponse = await container.ReplaceItemAsync(itemBody, itemBody.Id, new PartitionKey(itemBody.Title));
            Console.WriteLine("Updated ProgramDetails [{0},{1}].\n \tBody is now: {2}\n", itemBody.Summary, itemBody.Id, programDetailsResponse.Resource);
        }
    }
}