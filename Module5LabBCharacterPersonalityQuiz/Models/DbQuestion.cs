using SQLite;

namespace Module5LabBCharacterPersonalityQuiz.Models
{

    [Table("questions")]
    public class DbQuestion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // [MinLength(3)]
        public string DisplayImage { get; set; }
        public string DisplayImageSemanticDescription { get; set; }

        [MaxLength(250)]

        [Unique]
        public string QuestionTitle { get; set; }

        [MaxLength(250)]
        public string Option1Msg { get; set; }

        [MaxLength(250)]
        public string Option2Msg { get; set; }

        public int Option1PersonalityKey { get; set; }
        public int Option1PersonalityValue { get; set; }
        public int Option2PersonalityKey { get; set; }
        public int Option2PersonalityValue { get; set; }

    }
}