using System.ComponentModel.DataAnnotations;
using Module5LabBCharacterPersonalityQuiz;

public class Question
{

    [Required]
    public string DisplayImage { get; set; }

    [Required]
    public string QuestionTitle { get; set; }
    
    [Required]
    public string Option1Msg { get; set; }
    
    [Required]
    public string Option2Msg { get; set; }
    
    [Required]
    public Dictionary<Personality, int> Option1PersonalitySet { get; set; }
    public Dictionary<Personality, int> Option2PersonalitySet { get; set; }
    

    public Question(string displayImage, string questionTitle, string option1Msg, string option2Msg, Dictionary<Personality, int> option1PersonalitySet, Dictionary<Personality, int> option2PersonalitySet){
        this.DisplayImage = displayImage;
        this.QuestionTitle = questionTitle;
        this.Option1Msg = option1Msg;
        this.Option2Msg = option2Msg;
        this.Option1PersonalitySet = option1PersonalitySet;
        this.Option2PersonalitySet = option2PersonalitySet;
    }
}
