using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PersonData")]
public class PersonData : ScriptableObject
{
    public Sprite Mugshot;
    public string Name;
    public int PersonID;
    public string Relation;
    public List<Testimony> Testimonies = new List<Testimony>();
    public List<string> Descriptions = new List<string>();
    public Manager manager;
    

}
