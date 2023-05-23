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
    public void OnEnable()
    {
        manager = FindObjectOfType<Manager>();
        Descriptions.AddRange(manager.GetTestimonyTextByID(PersonID));
        Debug.Log("OnEnable " + Name);
    }

}
