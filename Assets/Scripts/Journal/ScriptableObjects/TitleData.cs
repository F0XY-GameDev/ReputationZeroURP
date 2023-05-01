using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TitleData", menuName = "ScriptableObjects/TitleData", order = 3)]

public class TitleData : ScriptableObject
{
    public string caseTitle;
    public List<string> victimNames = new List<string>();
    public List<Sprite> victimSprites = new List<Sprite>();
    public string caseDescription;
}
