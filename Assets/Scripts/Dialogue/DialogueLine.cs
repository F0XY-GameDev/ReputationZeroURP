using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DialogueLine")]
public class DialogueLine : ScriptableObject, IDiscoverable, IHideable //as an IDiscoverable and IHideable, dialoguelines can both be placed into the journal, and 
{
    public int DialogueID;
    public string PersonName;
    public int PersonID;
    public string DialogueText;
    public bool IsTestimony;
    public int TestimonyID;
    public bool HasCondition;
    public bool IsConditionMet;
    public int ConditionID;
    public bool changesAfterHearing;
    public bool canHear;
    public bool Discovered;
    public bool Hidden;
    public bool endsDialogue;
    public bool spawnsJournal;
    public AudioClip voiceLine;
    public UnityEvent OnSay;
    public List<int> responseIDs = new List<int>();
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }
    int IDiscoverable.ID { get => DialogueID; }
    bool IHideable.Hidden { get => Hidden; set => Hidden = value; }
    int IHideable.ID { get => DialogueID; }
    public void Say()
    {
        if (IsTestimony)
        {
            var manager = FindObjectOfType<Manager>();            
            manager.UnlockTestimonyByID(TestimonyID);
        }
        OnSay.Invoke();
    }
}
