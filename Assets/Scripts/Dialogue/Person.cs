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
    public List<DialogueLine> Lines;
    public List<Question> Responses;
    public DialogueDisplay dialogueDisplay;
    public Canvas displayCanvas;
    public UnityEvent OnTalk;
    public InputActionProperty toggleReference;
    public InputActionReference controls;
    public InputActionAsset ActionAsset;

    bool isColliding = false;

    public bool Discovered;
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }

    void Awake()
    {
        Lines = new List<DialogueLine>();
        Responses = new List<Question>();
        toggleReference.action.started += Interact;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Interact;
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
            Discovered = true;
        }
    }

    void Start()
    {
        dialogueDisplay.Hide();
        List<DialogueLine> tempDialogueList = new List<DialogueLine>();
        foreach (DialogueLine dialogue in FindObjectOfType<DialogueManager>().LoadedDialogueConfig.Lines)
        {
            if (dialogue.PersonName == PersonData.Name && dialogue.canHear)
            {
                tempDialogueList.Add(dialogue);
            }
        }
        Lines.AddRange(tempDialogueList);

        List<Question> tempQuestionList = new List<Question>();
        foreach (Question question in FindObjectOfType<ResponseManager>().LoadedQuestionConfig.Questions)
        {
            if (question.PersonName == PersonData.Name && question.isConditionMet)
            {
                tempQuestionList.Add(question);
            }
        }
        Responses.AddRange(tempQuestionList);
        
        
    }

    private void OnTriggerStay(Collider other)
    {                
        if (dialogueDisplay.enabled) { return; }        
        if (controls.action.ReadValue<float>() >= 0.7f)
        {
            Debug.Log("buttonPress");
            OnTalk.Invoke();
            FindObjectOfType<Journal>().UpdatePersons();
            Discovered = true;            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }


    public void UnlockDialogue(DialogueLine dialogue) //passed a dialogueline by the gamestatemanager when a new line is unlocked, then sorts dialogue by ID
    {
        Lines.Add(dialogue);
        Lines = Lines.OrderBy(d => d.DialogueID).ToList();
    }

    public void UnlockQuestion(Question question)
    {
        Responses.Add(question);
        Responses = Responses.OrderBy(d => d.ID).ToList();
    }

    public void EngageDialogue(GameObject player)
    {
        OnTalk.Invoke();
        FindObjectOfType<Journal>().UpdatePersons();
        Discovered = true;
    }
}
