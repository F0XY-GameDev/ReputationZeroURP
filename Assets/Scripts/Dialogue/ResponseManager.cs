using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;


public class QuestionConfig
{
    public List<Question> Questions;
    public QuestionConfig()
    {
        Questions = new List<Question>();
    }
}

public class ResponseManager : MonoBehaviour //dialoguemanager holds the dialogue for each scene, it reads the filename based off which scene we are in
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    [SerializeField] string filename; 
    public QuestionConfig LoadedQuestionConfig;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] List<Question> allQuestions = new List<Question>();
    [SerializeField] List<IHideable> discoverableQuestions = new List<IHideable>();
    public void Awake()
    {
        allQuestions = allQuestions.OrderBy(x => x.ID).ToList();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            //CHECK BUILD INDEX TO FIND WHICH SCENE IS WHERE
            case (0):
                filename = "mansionQuestions.json";
                break;
            case (1):
                filename = "mansionQuestions.json";
                break;
            default:
                break;
        }
        var fullPath = Path.Combine(Application.persistentDataPath, filename);
        if (File.Exists(fullPath))
        {
            using (var inStream = new FileStream(fullPath, FileMode.Open))
            {
                using (var reader = new StreamReader(inStream))
                {
                    LoadedQuestionConfig = JsonConvert.DeserializeObject<QuestionConfig>(reader.ReadToEnd());
                }
            }
        }
        else
        {
            var defConfig = new QuestionConfig();
            defConfig.Questions.AddRange(allQuestions);            
            //defConfig.Lines.AddRange();
            using (var outStream = new FileStream(fullPath, FileMode.Create))
            {
                using (var writer = new StreamWriter(outStream))
                {
                    writer.Write(JsonConvert.SerializeObject(defConfig));
                    writer.Flush();
                }
            }
            LoadedQuestionConfig = defConfig;
        }
    }
    public Question GetQuestionByID(int id)
    {
        return allQuestions[id];
    }
    public void UnlockQuestionByID(int id)
    {
        for (int i = 0; i < LoadedQuestionConfig.Questions.Count - 1; i++)
        {
            if (LoadedQuestionConfig.Questions[i].ID == id)
            {
                return;
            }
        }
        var question = GetQuestionByID(id);
        var person = FindPersonByID(question.PersonID);
        person.UnlockQuestion(question);
        dialogueManager.UnlockDialogueByID(question.dialogueIDs[0]);
    }
    private Person FindPersonByID(int id)
    {
        var allPersons = FindObjectsOfType<Person>();
        foreach (Person person in allPersons)
        {
            if (person.PersonData.PersonID == id)
            {
                return person;
            }
        }
        return null;
    }
    public void UpdateQuestions()
    {
        var tempList = new List<Question>();
        foreach (IHideable hideable in discoverableQuestions)
        {
            tempList.Add((Question)hideable);
        }
        allQuestions = tempList;
        allQuestions = allQuestions.OrderBy(x => x.ID).ToList();
        LoadedQuestionConfig.Questions = allQuestions;
    }
}

