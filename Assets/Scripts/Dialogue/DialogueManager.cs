using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;


public interface IHideable
{
    public bool Hidden { get; set; }
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
    [SerializeField] List<DialogueLine> AllDialogue = new List<DialogueLine>();

    public void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            //CHECK BUILD INDEX TO FIND WHICH SCENE IS WHERE
            case (0):
                filename = "partyDialogue.json";
                break;
            case (1):
                filename = "mansionDialogue.json";
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
                    LoadedDialogueConfig = JsonConvert.DeserializeObject<DialogueConfig>(reader.ReadToEnd());
                }
            }
        }
        else
        {
            var defConfig = new DialogueConfig();
            defConfig.Lines.AddRange(AllDialogue);
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
}


