using System.ComponentModel.DataAnnotations;
using Module5LabBCharacterPersonalityQuiz;

public class Result
{

    [Required]
    public string DisplayImage { get; set; }

    [Required]
    public string DisplayImageDescription { get; set; }

    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }

    [Required]
    public Personality[] PersonalityOrder;

    public Result(string displayImage, string displayImageDescription, string Title, string Description, Personality[] personalityOrder){
        this.DisplayImage = displayImage;
        this.DisplayImageDescription = displayImageDescription;
        this.Title = Title;
        this.Description = Description;
        this.PersonalityOrder = personalityOrder;
    }

    public bool CheckResultMatch(Personality[] againstPersonalityOrder){
        for(int i=0; i<againstPersonalityOrder.Length; i++){
            if(againstPersonalityOrder[i] != this.PersonalityOrder[i]){
                return false;
            }
        }
        return true;
    }
}
