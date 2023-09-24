using Newtonsoft.Json;

namespace ApplicationSystem.Models
{
    public class Workflow
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = string.Empty;

        public string StageName { get; set; } = string.Empty;

        public List<StageType> StageTypes { get; set; } = new();

        public string ProgramId { get; set; } = string.Empty;
    }

    public class StageType
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsVisibleToCandidate { get; set; }
    }
}