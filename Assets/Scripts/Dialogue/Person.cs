using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(MeshCollider))]
public class Person : MonoBehaviour, IDiscoverable
{
    // Never forget: Start is called before the first frame update and Update is called once per frame
    public PersonData PersonData;
    public List<DialogueLine> Lines;
    public List<Question> Responses;
    public DialogueDisplay dialogueDisplay;
    public UnityEvent OnTalk;

    public bool Discovered;
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }

    void Awake()
    {
        Lines = new List<DialogueLine>();
        Responses = new List<Question>();
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
        if (dialogueDisplay.isActive) { return; }
        if (Input.GetButtonDown("XRI_Right_PrimaryButton"))
        {
            Debug.Log("buttonPress");
            OnTalk.Invoke();
            FindObjectOfType<Journal>().UpdatePersons();
            Discovered = true;            
        }
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
}
