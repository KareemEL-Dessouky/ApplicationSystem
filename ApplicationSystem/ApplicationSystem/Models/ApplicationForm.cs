using Newtonsoft.Json;

namespace ApplicationSystem.Models
{
    public class ApplicationForm
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = string.Empty;

        public string CoverImage { get; set; } = string.Empty;

        public PersonalInformation PersonalInformation { get; set; } = new();

        public Profile Profile { get; set; } = new();

        public List<AdditionalQuestions> AdditionalQuestions { get; set; } = new();

        public string ProgramId { get; set; } = string.Empty;
    }

    public class PersonalInformation
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<Information> Information { get; set; } = new();
    }

    public class Profile
    {
        public Education Education { get; set; } = new();

        public Experience Experience { get; set; } = new();

        public string Resume { get; set; } = string.Empty;
    }

    public class AdditionalQuestions
    {
        public string Question { get; set; } = string.Empty;

        public string QuestionType { get; set; } = string.Empty;

        public List<string> Answers { get; set; } = new();
    }

    public class Information
    {
        public string Title { get; set; } = string.Empty;

        public bool IsInternal { get; set; }

        public bool IsHidden { get; set; }
    }

    public class Education
    {
        public string School { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;

        public string Course { get; set; } = string.Empty;

        public string StudyLocation { get; set; } = string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsCurrentlyThere { get; set; }
    }

    public class Experience
    {
        public string Company { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsCurrentlyThere { get; set; }        
    }
}