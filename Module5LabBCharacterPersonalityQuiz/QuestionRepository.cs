using SQLite;
using Module5LabBCharacterPersonalityQuiz.Models;
using System.Diagnostics;


namespace Module5LabBCharacterPersonalityQuiz;

public class QuestionRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    private void Init()
    {
        
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);

        // conn.Execute($"DROP TABLE IF EXISTS questions");

        conn.CreateTable<DbQuestion>(); 

    }

    public QuestionRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public void AddNewDbQuestion(Question question){
		this.AddNewDbQuestion(
            question.DisplayImage,
            question.QuestionTitle,
            question.Option1Msg,
            question.Option2Msg,
            question.Option1PersonalitySet.First().Key,
            question.Option1PersonalitySet.First().Value,
            question.Option2PersonalitySet.First().Key,
            question.Option2PersonalitySet.First().Value
        );

    }

    public void AddNewDbQuestion(string displayImagePath, string questionTitle, string option1Msg, string option2Msg, Personality option1PersonalityKey, int option1PersonalityValue, Personality option2PersonalityKey, int option2PersonalityValue)
    {            
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(displayImagePath))//TODO: Make check that all are undercase, there are no spaces, and that it is in a valid file-extension
                throw new Exception("Valid QuestionTitle required");
            if (string.IsNullOrEmpty(questionTitle))
                throw new Exception("Valid QuestionTitle required");
            if (string.IsNullOrEmpty(option1Msg))
                throw new Exception("Valid Option1Msg required");
            if (string.IsNullOrEmpty(option2Msg))
                throw new Exception("Valid Option2Msg required");

            result = conn.Insert(new DbQuestion {
				DisplayImage = displayImagePath,
				QuestionTitle = questionTitle,
				Option1Msg = option1Msg,
				Option2Msg = option2Msg,
				Option1PersonalityKey = (int)option1PersonalityKey,
				Option1PersonalityValue = option1PersonalityValue,
				Option2PersonalityKey = (int)option2PersonalityKey,
				Option2PersonalityValue = option2PersonalityValue,
            });

            StatusMessage = string.Format("{0} record(s) added question (Title: {1})", result, questionTitle);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0} with. Error: {1}", questionTitle, ex.Message);
        }

    }

    public List<DbQuestion> GetAllDbQuestions()
    {
        try
        {
            Init();
            Debug.WriteLine("conn.Table<DbQuestion>().ToList().Count()");
            Debug.WriteLine("conn.Table<DbQuestion>().ToList().Count() _"+conn.Table<DbQuestion>().ToList().Count());

            return conn.Table<DbQuestion>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<DbQuestion>();
    }

    public List<Question> GetAllQuestions(){
        List<Question> questions = new List<Question>();

        List<DbQuestion> dbQuestionsFromDb = this.GetAllDbQuestions();

        Debug.WriteLine("dbQuestionsFromDbdbQuestionsFromDb");
        Debug.WriteLine("dbQuestionsFromDbdbQuestionsFromDb_"+dbQuestionsFromDb.Count());
        foreach(DbQuestion dbQuestion in dbQuestionsFromDb){
            questions.Add(new Question(
                dbQuestion.DisplayImage,
                dbQuestion.QuestionTitle,
                dbQuestion.Option1Msg,
                dbQuestion.Option2Msg,
                new Dictionary<Personality, int>{{ (Personality)dbQuestion.Option1PersonalityKey, dbQuestion.Option1PersonalityValue}},
                new Dictionary<Personality, int>{{ (Personality)dbQuestion.Option2PersonalityKey, dbQuestion.Option2PersonalityValue}}
            ));
        }
        return questions;
    }

}
