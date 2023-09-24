using ApplicationSystem.Models;
using Microsoft.Azure.Cosmos;

namespace ApplicationSystem.Services
{
    // Tab 3
    public class WorkflowService
    {
        private readonly Database database;
        private readonly Container container;

        public WorkflowService()
        {
            database = Program.client.GetDatabase("application");
            container = database.GetContainer("workflow");
        }

        // Get
        public async Task GetItem()
        {
            var sqlQueryText = "SELECT * FROM w WHERE w.ProgramId = 'd58deb83-99ae-4b0e-bc7c-b633a7a78dbe'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new(sqlQueryText);
            FeedIterator<Workflow> queryResultSetIterator = container.GetItemQueryIterator<Workflow>(queryDefinition);

            List<Workflow> workflows = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Workflow> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Workflow workflow in currentResultSet)
                {
                    workflows.Add(workflow);
                    Console.WriteLine("\tRead {0}\n", workflow);
                }
            }
        }


        // Put
        public async Task ReplaceItem()
        {
            ItemResponse<Workflow> workflowResponse = await container.ReadItemAsync<Workflow>("jefief348-12fh-h5u7-bx6x-ifeef2khfj2e", new PartitionKey("jefief348-12fh-h5u7-bx6x-ifeef2khfj2e"));

            var itemBody = workflowResponse.Resource;

            itemBody.StageName = "Test Stage Name";

            // replace the item with the updated content.
            workflowResponse = await container.ReplaceItemAsync(itemBody, itemBody.Id, new PartitionKey(itemBody.Id));
            Console.WriteLine("Updated Workflow [{0},{1}].\n \tBody is now: {2}\n", itemBody.StageName, itemBody.Id, workflowResponse.Resource);
        }
    }
}