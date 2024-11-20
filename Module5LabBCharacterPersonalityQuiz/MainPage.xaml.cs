using System.Diagnostics;
using Module5LabBCharacterPersonalityQuiz.Models;

namespace Module5LabBCharacterPersonalityQuiz;

public enum Personality 
{
	Transhumanism,
	Expansion,
	NobileGoals,
}


public partial class MainPage : ContentPage
{

	int questionIndex = -1;

	Dictionary<Personality, int> personalityTracking = new Dictionary<Personality, int>{
		{ Personality.Transhumanism, 0},
		{ Personality.Expansion, 0},
		{ Personality.NobileGoals, 0},
	};


	List<Question> questions;
			

	public MainPage()
	{
		InitializeComponent();
		Debug.WriteLine("_______Personalties_______");
		Debug.WriteLine("Expansion: "+(int)Personality.Expansion);
		Debug.WriteLine("NobileGoals: "+(int)Personality.NobileGoals);
		Debug.WriteLine("Transhumanism: "+(int)Personality.Transhumanism);
		Debug.WriteLine("__________________________");

        List<Question> addIfNotAlreadyInDbQuestions = new List<Question>();

		addIfNotAlreadyInDbQuestions.Add(new Question("hourglass.jpg", "Longivitiy", "For expanded longivity", "Against expanded longivity",
			new Dictionary<Personality, int>{
				{ Personality.Transhumanism, 1},
				{ Personality.NobileGoals, 5},
			},
			new Dictionary<Personality, int>{
				{ Personality.Transhumanism, -2},
				{ Personality.NobileGoals, -3},
			}
		));
		addIfNotAlreadyInDbQuestions.Add(new Question("hourglass.jpg", "Want to live...", ">150 years", "<150 years",
			new Dictionary<Personality, int>{
				{ Personality.Transhumanism, 5},
				{ Personality.Expansion, 1},
			},
			new Dictionary<Personality, int>{
				{ Personality.Expansion, -1},
			}
		));
		addIfNotAlreadyInDbQuestions.Add(new Question("solar_system_map.jpg", "Travel Choice", "Off Earth", "On Earth",
			new Dictionary<Personality, int>{
				{ Personality.Expansion, 5},
			},
			new Dictionary<Personality, int>{
				{ Personality.Expansion, -1},
			}
		));
		addIfNotAlreadyInDbQuestions.Add(new Question("humanitarian.jpg", "Out to Help", "Activally Pursuing", "Doing my Own Thing",
			new Dictionary<Personality, int>{
				{ Personality.NobileGoals, 4},
			},
			new Dictionary<Personality, int>{
				{ Personality.NobileGoals, 0},
			}
		));

		// int i=0;
        foreach (Question addIfNotAlreadyInDbQuestion in addIfNotAlreadyInDbQuestions){
			App.QuestionRepo.AddNewDbQuestion(addIfNotAlreadyInDbQuestion);
        }

		questions = App.QuestionRepo.GetAllQuestions();

		// moveNext(false);
		// SwipeGestureRecognizer swipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left | SwipeDirection.Right };
		// swipeGesture.Swiped += OnSwiped;

		// SwipeArea.GestureRecognizers.Add(swipeGesture);
	}

	public void OnLeftSwipe(object sender, EventArgs e)
	{
		if(this.questionIndex < questions.Count()){
			moveNext(true);
		}
	}

	public void OnRightSwipe(object sender, EventArgs e)
	{
		if(this.questionIndex < questions.Count()){
			moveNext(false);
		}
	}

	void OnSwiped(object sender, SwipedEventArgs e)
	{
		Debug.WriteLine("!!!!!!_ONSWIPED");
		switch (e.Direction)
		{
			case SwipeDirection.Left:
				OnLeftSwipe(sender, e);
				break;
			case SwipeDirection.Right:
				OnRightSwipe(sender, e);
				break;
			case SwipeDirection.Up:
				// Handle the swipe
				break;
			case SwipeDirection.Down:
				// Handle the swipe
				break;
		}
	}

	private void moveNext(bool left){
		Debug.WriteLine("MoveNEXT = "+left);

		if(questionIndex >= 0){
			foreach (var personality in left? questions[questionIndex].Option1PersonalitySet : questions[questionIndex].Option2PersonalitySet)
			{
				personalityTracking[personality.Key] += personality.Value;
			}
		}
		
		questionIndex++;

		if(questionIndex < questions.Count()){
			QuestionTitle.Text = questions[questionIndex].QuestionTitle;
			QuestionImg.Source = questions[questionIndex].DisplayImage;
			OptionLeft.Text = questions[questionIndex].Option1Msg;
			OptionRight.Text = questions[questionIndex].Option2Msg;
			Debug.WriteLine("!!!!!!"+questions[questionIndex].QuestionTitle);
		}else{
			results();
		}

	}

	private void results(){
		Result[] results = [
			// string displayImage, string Title, string Description, Personality[] personalityOrder
			new Result("we_are_legion.jpg", "(One of the many copies of Bob)", "From the Bobverse series", new Personality[]{ Personality.NobileGoals, Personality.Transhumanism, Personality.Expansion}),
			new Result("attack_surface.jpg", "Masha Maximow", "Attack Surface", new Personality[]{ Personality.NobileGoals, Personality.Expansion, Personality.Transhumanism}),
			new Result("mind_machines.jpg", "Mike Cohen", "Mind Machines (Humans++ series)", new Personality[]{ Personality.Transhumanism, Personality.NobileGoals, Personality.Expansion}),
			new Result("house_of_suns.jpg", "The Spirit of the Air / Hesperus", "House of Suns", new Personality[]{ Personality.Transhumanism, Personality.Expansion, Personality.NobileGoals}),
			new Result("the_hitchikers_guide_to_the_galaxy.jpg","Ford Perfect","The Hitchikers Guide to the Galaxy", new Personality[]{ Personality.Expansion, Personality.NobileGoals, Personality.Transhumanism}),
			new Result("house_of_suns.jpg", "The Gentian Line (Orignally Abigail Gentian)", "House of Suns", new Personality[]{ Personality.Expansion, Personality.Transhumanism, Personality.NobileGoals}),
		];


		Personality[] checkPersonalityOrder = new Personality[personalityTracking.Count()];

		// SortedDictionary<Personality, int> orderedPersonalityTracking = new SortedDictionary<Personality, int>(personalityTracking);
		
		// Sorting `personalityTracking` by values in descending order and storing it in a new dictionary
		var orderedPersonalityTracking = personalityTracking.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);

		int orderPlaceIndex = 0;
		foreach(var personalityTracked in orderedPersonalityTracking){
			checkPersonalityOrder[orderPlaceIndex++] = personalityTracked.Key;
		}
		Debug.WriteLine("Check Against");

		Debug.WriteLine(checkPersonalityOrder[0]);
		Debug.WriteLine(checkPersonalityOrder[1]);
		Debug.WriteLine(checkPersonalityOrder[2]);

		Debug.WriteLine("Results");
		foreach(var result in results){
		Debug.WriteLine("!!!!!!"+result.Title);
		Debug.WriteLine(result.PersonalityOrder[0]);
		Debug.WriteLine(result.PersonalityOrder[1]);
		Debug.WriteLine(result.PersonalityOrder[2]);

			if(result.CheckResultMatch(checkPersonalityOrder)){
				QuestionImg.Source = result.DisplayImage;
				QuestionTitle.Text = result.Title;
				QuestionDescription.Text = result.Description;
				OptionLeft.IsVisible = false;
				OptionRight.IsVisible = false;
				return;
			}
		}

		QuestionImg.Source = "dotnet_bot";
		QuestionTitle.Text = "Sorry, but we don't have a result for that ordering, :(";
		OptionLeft.IsVisible = false;
		OptionRight.IsVisible = false;
	}

	public void OnResetQuiz(object sender, EventArgs e){
		Debug.WriteLine(this.personalityTracking[Personality.Transhumanism].ToString()+"__"+this.personalityTracking[Personality.NobileGoals].ToString()+"__"+this.personalityTracking[Personality.Expansion].ToString());

		this.questionIndex = -1;
		this.personalityTracking = new Dictionary<Personality, int>{
			{ Personality.Transhumanism, 0},
			{ Personality.Expansion, 0},
			{ Personality.NobileGoals, 0},
		};
		QuestionImg.Source = "dotnet_bot";
		QuestionTitle.Text = "";
		OptionLeft.IsVisible = true;
		OptionRight.IsVisible = true;
		QuestionDescription.Text = "";

		moveNext(false);
	}
}

