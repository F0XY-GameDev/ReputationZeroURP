using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueDisplay : MonoBehaviour
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    [SerializeField] TextMeshProUGUI NameTMP;
    [SerializeField] TextMeshProUGUI DialogueTextTMP;
    int currentDialogueID;    
    Person currentSpeaker;
    public bool isActive;
    [SerializeField] ResponseDisplay responseDisplay;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject xCanvas;
    [SerializeField] GameObject yCanvas;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Manager manager;
    
    
    

    public void Awake()
    {
        manager = FindObjectOfType<Manager>();
        responseDisplay = FindObjectOfType<ResponseDisplay>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        canvas.SetActive(false);
        if (xCanvas != null)
        {
            xCanvas.SetActive(true);
        }
        if (yCanvas != null)
        {
            yCanvas.SetActive(false);
        }
    }    

    public void ClearText()
    {
        NameTMP.text = " ";
        DialogueTextTMP.text = " ";
    }
    

    public void BeginDialogue(Person person) //begin dialogue is called by the person interacted with to it's own attached dialoguedisplay, it then passes the data from the person to it's own display and that of the responsedisplay
    {
        if (manager.xTutorialFinished)
        {
            xCanvas.SetActive(false);
        }
        if (manager.yTutorialFinished)
        {
            yCanvas.SetActive(false);
        }
        Debug.Log("DialogueInitiated");
        Show();
        currentSpeaker = person;
        responseDisplay.SetSpeaker(person);
        SetText(person.Lines[0]);
        currentDialogueID = person.Lines[0].DialogueID;
        responseDisplay.BeginResponse(person.Lines[0], this);
    }

    
    public void AdvanceDialogueByResponse(Question question) //When the player sets a response, that question is passed to the dialogue and handled based off the IDs in each question and dialogueline
    {
        if (!manager.xTutorialFinished)
        {
            manager.xTutorialFinished = true;
            xCanvas.SetActive(false);
            yCanvas.SetActive(true);
        }
        if (question.endsDialogue)
        {
            EndDialogue();
        }
        var dialogue = dialogueManager.GetLineByID(question.dialogueIDs[question.dialogueIDs.Count - 1]);
        SetText(dialogue);        
        currentDialogueID = dialogue.DialogueID;
        if (dialogue.endsDialogue)
        {
            EndDialogue();
        }
        responseDisplay.SetText(dialogue);
    }

    public void EndDialogue()
    {
        StartCoroutine(responseDisplay.EndResponseAfterSeconds(0.1f));
        StartCoroutine(EndDialogueAfterSeconds(0.1f));
    }

    public IEnumerator EndDialogueAfterSeconds(float seconds)
    {
        manager.yTutorialFinished = true;
        yield return new WaitForSeconds(seconds);
        ClearText();
        currentSpeaker = null;
        Hide();        
    }

    public void SetText(DialogueLine dialogue)
    {
        NameTMP.text = dialogue.PersonName;
        DialogueTextTMP.text = dialogue.DialogueText;
    }

    public void SetDialogueID(int value)
    {
        currentDialogueID = value;
    }

    public void Show()
    {
        isActive = true;
        canvas.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;
        canvas.SetActive(false);
    }

    /* DEPRECATED OR UNUSED BELOW

    public void AdvanceDialogue()
    {
        if (currentDialogueID == currentSpeaker.Lines.Count) { EndDialogue(); }
        var nextDialogue = currentSpeaker.Lines[currentDialogueID + 1];
        currentDialogueID++;
        SetText(nextDialogue);
    }
    */
}