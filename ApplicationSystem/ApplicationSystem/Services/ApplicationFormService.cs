using ApplicationSystem.Models;
using Microsoft.Azure.Cosmos;

namespace ApplicationSystem.Services
{
    // Tab 2
    public class ApplicationFormService
    {
        private readonly Database database;
        private readonly Container container;

        public ApplicationFormService()
        {
            database = Program.client.GetDatabase("application");
            container = database.GetContainer("applicationForm");
        }

        // Get
        public async Task GetItem()
        {
            var sqlQueryText = "SELECT * FROM a WHERE a.ProgramId = 'd58deb83-99ae-4b0e-bc7c-b633a7a78dbe'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new(sqlQueryText);
            FeedIterator<ApplicationForm> queryResultSetIterator = container.GetItemQueryIterator<ApplicationForm>(queryDefinition);

            List<ApplicationForm> applicationForms = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ApplicationForm> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (ApplicationForm applicationForm in currentResultSet)
                {
                    applicationForms.Add(applicationForm);
                    Console.WriteLine("\tRead {0}\n", applicationForm);
                }
            }
        }


        // Put
        public async Task ReplaceItem()
        {
            ItemResponse<ApplicationForm> applicationFormResponse = await container.ReadItemAsync<ApplicationForm>("d5fehj348-18hj-4b0e-bc7c-b6jduelg8dbe", new PartitionKey("d5fehj348-18hj-4b0e-bc7c-b6jduelg8dbe"));

            var itemBody = applicationFormResponse.Resource;

            itemBody.PersonalInformation.LastName = "El-Dessouky";

            // replace the item with the updated content.
            applicationFormResponse = await container.ReplaceItemAsync(itemBody, itemBody.Id, new PartitionKey(itemBody.Id));
            Console.WriteLine("Updated ApplicationForm [{0},{1}].\n \tBody is now: {2}\n", itemBody.PersonalInformation.LastName, itemBody.Id, applicationFormResponse.Resource);
        }
    }
}