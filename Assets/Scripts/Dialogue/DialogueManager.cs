using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;


public interface IHideable
{
    public bool Hidden { get; set; }
    public int ID { get; }
}

public interface IConditional
{
    int ID { get; }
    bool isComplete { get; set; }
}
public class DialogueConfig
{
    public List<DialogueLine> Lines;
    public DialogueConfig()
    {
        Lines = new List<DialogueLine>();
    }    
}



public class DialogueManager : MonoBehaviour //dialoguemanager holds the dialogue for each scene, it reads the filename based off which scene we are in
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    [SerializeField] string filename;
    public DialogueConfig LoadedDialogueConfig;
    [SerializeField] ResponseManager responseManager;
    [SerializeField] List<DialogueLine> allDialogue = new List<DialogueLine>();
    [SerializeField] List<IHideable> discoverableDialogue = new List<IHideable>();
    public void Awake()
    {
        allDialogue = allDialogue.OrderBy(x => x.DialogueID).ToList();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            //CHECK BUILD INDEX TO FIND WHICH SCENE IS WHERE
            case (0):
                filename = "mansionDialogue.json";
                break;
            case (1):
                filename = "mansionDialogue.json";
                break;
            default:
                filename = "mansionDialogue.json";
                break;
        }

        var fullPath = Path.Combine(Application.persistentDataPath, filename);

        
        if (File.Exists(fullPath))
        {
            var defConfig = new DialogueConfig();
            defConfig.Lines.AddRange(allDialogue);
            LoadedDialogueConfig = defConfig;
        }
        else
        {
            var defConfig = new DialogueConfig();
            defConfig.Lines.AddRange(allDialogue);
            //defConfig.Lines.AddRange();
            using (var outStream = new FileStream(fullPath, FileMode.Create))
            {
                using (var writer = new StreamWriter(outStream))
                {
                    writer.Write(JsonConvert.SerializeObject(defConfig));
                    writer.Flush();
                }
            }
            LoadedDialogueConfig = defConfig;
        }
    }
    public DialogueLine GetLineByID(int id)
    {
        return LoadedDialogueConfig.Lines[id];
    }
    public void UnlockDialogueByID(int id)
    {
        for (int i = 0; i < LoadedDialogueConfig.Lines.Count - 1; i++)
        {
            if (LoadedDialogueConfig.Lines[i].DialogueID == id)
            {
                return;
            }
        }
        var dialogue = GetLineByID(id);
        var person = FindPersonByID(dialogue.PersonID);
        person.UnlockDialogue(dialogue);
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
    public void UpdateDialogue()
    {
        var tempList = new List<DialogueLine>();
        foreach (IHideable hideable in discoverableDialogue)
        {
            tempList.Add((DialogueLine)hideable);
        }
        allDialogue = tempList;
        allDialogue = allDialogue.OrderBy(x => x.DialogueID).ToList();
        LoadedDialogueConfig.Lines = allDialogue;
    }
}


