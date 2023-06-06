using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IPerson { };
public interface IEvidence { };

public class GameProp : MonoBehaviour, IDiscoverable
{
    public bool discovered;
    public int ID;
    bool IDiscoverable.Discovered { get => discovered; set => discovered = value; }

    int IDiscoverable.ID { get => ID; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
