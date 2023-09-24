using Newtonsoft.Json;

namespace ApplicationSystem.Models
{
    public class ProgramDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Summary { get; set; }

        public string Description { get; set; } = string.Empty;

        public List<string>? RequiredSkills { get; set; }

        public string? Benefits { get; set; } = string.Empty;

        public string? ApplicationCriteria { get; set; } = string.Empty;

        public AdditionalProgramInfo AdditionalInfo { get; set; } = new();

        public override string ToString()
        {
            return $"Id: {Id} \t Title: {Title}";
        }
    }


    public class AdditionalProgramInfo
    {
        public string Type { get; set; } = string.Empty;

        public DateTime? Start { get; set; }

        public DateTime ApplicationOpen { get; set; }

        public DateTime ApplicationClose { get; set; }

        public int? Duration { get; set; }

        public string Location { get; set; } = string.Empty;

        public string? MinQulifications { get; set; }

        public int? ApplicationMaxNumber { get; set; }
    }
}