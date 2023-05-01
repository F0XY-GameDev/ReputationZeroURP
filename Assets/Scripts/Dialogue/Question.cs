using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Question")]
public class Question : ScriptableObject, IHideable, IConditional
{
    public int ID;
    public string Message;
    public string PersonName;
    public bool HasCondition;
    public bool isConditionMet;
    public int ConditionID;
    public bool Hidden;
    public bool endsDialogue;
    public List<int> dialogueIDs = new List<int>();
    bool IHideable.Hidden { get => Hidden; set => Hidden = value; }
    int IConditional.ID { get => ID; }
    bool IConditional.isComplete { get => isConditionMet; set => isConditionMet = value; }
}
