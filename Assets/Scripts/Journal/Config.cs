using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class GameConfig
{
    public List<DialogueLine> Lines;
    public GameConfig()
    {
        Lines = new List<DialogueLine>();
    }
}

public class Config : MonoBehaviour
{
    string filename = "gameconfig.json";


    public GameConfig LoadedConfig;
    // Start is called before the first frame update
    private void Awake()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, filename);



        if (File.Exists(fullPath))
        {
            using (var inStream = new FileStream(fullPath,FileMode.Open))
            {
                using (var reader = new StreamReader(inStream))
                {
                    LoadedConfig = JsonConvert.DeserializeObject<GameConfig>(reader.ReadToEnd());
                }
            }
        }
        else
        {
            var defConfig = new GameConfig();

            //
            defConfig.Lines.Add(new DialogueLine() { PersonName = "Jana", DialogueText = "Hello" });
            defConfig.Lines.Add(new DialogueLine() { PersonName = "Mihai", DialogueText = "Hello" });
            defConfig.Lines.Add(new DialogueLine() { PersonName = "Niklas", DialogueText = "Bye bye" });

            using (var outStream = new FileStream(fullPath, FileMode.Create))
            {
                using (var writer = new StreamWriter(outStream))
                {
                    writer.Write(JsonConvert.SerializeObject(defConfig));
                    writer.Flush();
                }
            }
            LoadedConfig = defConfig;

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    





}
