namespace Module5LabBCharacterPersonalityQuiz;

using Module5LabBCharacterPersonalityQuiz.Models;

public partial class App : Application
{

	public static QuestionRepository QuestionRepo { get; set; }

	public App(QuestionRepository questionRepo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		QuestionRepo = questionRepo;

	}
	

}
