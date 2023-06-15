using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Testimony")]
public class Testimony : ScriptableObject, IHiddenDescription
{
    public int PersonID;
    public int TestimonyID;
    public string Text;
    public bool Discovered;
    public int ConditionID;
    int IHiddenDescription.TargetID { get => PersonID; }
    string IHiddenDescription.Message { get => Text; }
    bool IHiddenDescription.Discovered { get => Discovered; set => Discovered = value; }
    int IHiddenDescription.OwnID { get => TestimonyID; }
    public void Discover()
    {
        Discovered = true;
    }
}
