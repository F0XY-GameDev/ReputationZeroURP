using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Events;

public class ResponseDisplay : MonoBehaviour
{
    [SerializeField] Transform[] UIBlockTransforms;
    [SerializeField] TMPro.TextMeshProUGUI[] UIBlockTMPs;    
    int currentQuestionID;
    [SerializeField]Person currentSpeaker;
    public bool isActive;
    [SerializeField] List<Question> currentAvailableQuestions = new List<Question>();
    [SerializeField] List<Question> allPossibleQuestions = new List<Question>();
    [SerializeField] GameObject[] responseBoxes;
    [SerializeField] ResponseManager responseManager;

    [SerializeField] DialogueManager dialogueManager;

    [SerializeField] DialogueDisplay dialogueDisplay;
    [SerializeField] Player player;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 attachOffset;
    [SerializeField] Vector3 scale;
    public UnityEvent OnStartSuspectChoice;
    public UnityEvent OnEndSuspectChoice;
    [SerializeField] Journal journal;
    public GameObject journalOBJ;
    public GameObject attachPoint;



    public void Awake()
    {
        foreach (GameObject gameObject in responseBoxes)
        {
            gameObject.SetActive(false);
        }
        responseManager = FindObjectOfType<ResponseManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueDisplay = FindObjectOfType<DialogueDisplay>();
    }

    public void SetSpeaker(Person person)
    {
        currentSpeaker = person;
    }

    public void ClearText()
    {
        foreach (TMPro.TextMeshProUGUI tmp in UIBlockTMPs)
        {
            tmp.text = " ";
        }
    }

    public void BeginResponse(DialogueLine dialogue, DialogueDisplay _dialogueDisplay) //to begin responses, the responsedisplay gets the possible responses for the current dialogue and displays them
    {
        player.isTalking = true;
        dialogueDisplay = _dialogueDisplay;
        if (dialogue.spawnsJournal)
        {
            journalOBJ.transform.position = attachPoint.transform.position;
            journalOBJ.transform.rotation = attachPoint.transform.rotation;
        }
        var transform = dialogueDisplay.GetComponentsInChildren<Transform>();
        foreach (Transform t in transform)
        {
            if (t.CompareTag("Attach"))
            {
                this.transform.parent = t;
                this.transform.localPosition = Vector3.zero + attachOffset;
                this.transform.localScale = scale;
                this.transform.localRotation = Quaternion.identity;
            }
        }
        Show();
        var tempQuestionList = new List<Question>();
        foreach(int i in dialogue.responseIDs) { tempQuestionList.Add(responseManager.GetQuestionByID(i)); Debug.Log("Added response"); }
        currentAvailableQuestions.AddRange(tempQuestionList);
        for (int i = 0; i < UIBlockTMPs.Length; i++)
        {
            if(currentAvailableQuestions[i] == null) { break; }
            UIBlockTMPs[i].text = currentAvailableQuestions[i].Message;            
        }        
    }

    public void EndResponses()
    {
        ClearText();
        currentSpeaker = null;
        Hide();
    }
        
    public void SelectTopResponse()
    {
        SelectResponse(0);
    }

    public void SelectRightResponse()
    {
        SelectResponse(1);
    }
    public void SelectLeftResponse()
    {
        SelectResponse(2);
    }

    void SelectResponse(int value) //when the player selects a response based off the above code, the response is passed to the dialoguedisplay
    {
        ClearText();
        dialogueDisplay.AdvanceDialogueByResponse(currentAvailableQuestions[value]);
        if (currentAvailableQuestions[value].startsSuspectChoosing)
        {
            OnStartSuspectChoice.Invoke();
        }
        if (currentAvailableQuestions[value].endsSuspectChoosing)
        {
            OnEndSuspectChoice.Invoke();
        }
    }

    public void SetText(DialogueLine dialogue)
    {        
        currentAvailableQuestions.Clear();
        var responseIDs = dialogue.responseIDs;
        var tempResponsesList = new List<Question>();
        foreach (int ID in responseIDs)
        {
            var question = responseManager.GetQuestionByID(ID);
            if (!question.Hidden)
            {
                tempResponsesList.Add(question);
            }
        }
        currentAvailableQuestions.AddRange(tempResponsesList);
        for (int i = 0; i < UIBlockTMPs.Length; i++)
        {
            UIBlockTMPs[i].text = currentAvailableQuestions[i].Message;
        }
    }
    public void SetQuestionID(int value)
    {
        currentQuestionID = value;
    }

    public void Show()
    {
        isActive = true;
        foreach (GameObject gameObject in responseBoxes)
        {
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        isActive = false;
        foreach (GameObject gameObject in responseBoxes)
        {
            gameObject.SetActive(false);
        }
    }    

    public void EndDialogue()
    {
        StartCoroutine(dialogueDisplay.EndDialogueAfterSeconds(0.1f));
        StartCoroutine(EndResponseAfterSeconds(0.1f));
        this.transform.parent = playerTransform;
    }

    public IEnumerator EndResponseAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ClearText();
        currentSpeaker = null;
        currentQuestionID = 0;
        currentAvailableQuestions.Clear();
        allPossibleQuestions.Clear();
        player.isTalking = false;
        Hide();
    }
}
