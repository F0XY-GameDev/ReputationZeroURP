using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(MeshCollider))]
public class Person : MonoBehaviour, IDiscoverable
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    public PersonData PersonData;
    public PersonData alternatePersonData;
    public List<DialogueLine> Lines;
    public List<Question> Responses;
    public DialogueDisplay dialogueDisplay;
    public DialogueManager dialogueManager;
    public string alternateFirstLine;
    public AudioClip alternateFirstLineVoice;
    public Canvas displayCanvas;
    public UnityEvent OnTalk;
    public UnityEvent OnPersonEnteredTrigger;
    public UnityEvent OnPersonExitedTrigger;
    public InputActionProperty toggleReference;
    public InputActionReference controls;
    public InputActionAsset ActionAsset;
    public int testimonyCount;
    bool isColliding = false;

    public bool Discovered;
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }
    int IDiscoverable.ID { get => PersonData.PersonID; }

    void Awake()
    {
        Lines = new List<DialogueLine>();
        Responses = new List<Question>();
        controls.action.started += Interact;
    }

    private void OnDestroy()
    {
        controls.action.started -= Interact;
    }

    private void OnEnable()
    {
        if (ActionAsset != null)
        {
            ActionAsset.Enable();
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (isColliding)
        {
            if (displayCanvas.enabled) { return; }
            Debug.Log("buttonPress");
            OnTalk.Invoke();
            FindObjectOfType<Journal>().UpdatePersons();            
        }
    }

    void Start()
    {
        dialogueDisplay.Hide();
        List<DialogueLine> tempDialogueList = new List<DialogueLine>();        
        foreach (DialogueLine dialogue in dialogueManager.LoadedDialogueConfig.Lines)
        {
            if (dialogue.PersonID == PersonData.PersonID && dialogue.canHear)
            {
                tempDialogueList.Add(dialogue);
            }
        }
        Lines.AddRange(tempDialogueList);

        List<Question> tempQuestionList = new List<Question>();
        foreach (Question question in FindObjectOfType<ResponseManager>().LoadedQuestionConfig.Questions)
        {
            if (question.PersonID == PersonData.PersonID && question.isConditionMet)
            {
                tempQuestionList.Add(question);
            }
        }
        Responses.AddRange(tempQuestionList);
        
        
    }

    public void UpdateDialogue()
    {
        dialogueDisplay.Hide();
        List<DialogueLine> tempDialogueList = new List<DialogueLine>();
        foreach (DialogueLine dialogue in dialogueManager.LoadedDialogueConfig.Lines)
        {
            if (dialogue.PersonID == PersonData.PersonID && dialogue.canHear)
            {
                tempDialogueList.Add(dialogue);
            }
        }
        Lines.Clear();
        Lines.AddRange(tempDialogueList);

        List<Question> tempQuestionList = new List<Question>();
        foreach (Question question in FindObjectOfType<ResponseManager>().LoadedQuestionConfig.Questions)
        {
            if (question.PersonID == PersonData.PersonID && question.isConditionMet)
            {
                tempQuestionList.Add(question);
            }
        }
        Responses.Clear();
        Responses.AddRange(tempQuestionList);
    }

    public void Discover()
    {
        Discovered = true;
    }

    private void OnTriggerStay(Collider other)
    {                
        if (dialogueDisplay.enabled) { return; }        
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("button2Press");
            OnTalk.Invoke();
            FindObjectOfType<Journal>().UpdatePersons();
                        
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("button0Press");
            OnTalk.Invoke();
            FindObjectOfType<Journal>().UpdatePersons();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            OnPersonEnteredTrigger.Invoke();
            player.colliderGameobject = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            OnPersonExitedTrigger.Invoke();
            player.colliderGameobject = null;
        }
    }


    public void UnlockDialogue(DialogueLine dialogue) //passed a dialogueline by the gamestatemanager when a new line is unlocked, then sorts dialogue by ID
    {
        Lines.Add(dialogue);
        testimonyCount++;
        Lines = Lines.OrderBy(d => d.DialogueID).ToList();
    }

    public void UnlockQuestion(Question question)
    {
        Responses.Add(question);
        Responses = Responses.OrderBy(d => d.ID).ToList();
    }

    public void EngageDialogue(GameObject player)
    {        
        Debug.Log("DialogueAccepted");
        OnTalk.Invoke();
        FindObjectOfType<Journal>().UpdatePersons();        
    }

    public void ChangeFirstLine()
    {
        Lines[0].DialogueText = alternateFirstLine;
        Lines[0].voiceLine = alternateFirstLineVoice;
    }

    public void SwapToPersonData(PersonData personData)
    {
        PersonData = personData;
        UpdateDialogue();
    }
}
