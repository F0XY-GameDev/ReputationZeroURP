using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openBook : MonoBehaviour
{
    public GameObject Cover;
    public HingeJoint myHinge;
    Journal journal;
    
    void Start()
    {
        var myHinge = Cover.GetComponent<HingeJoint>();
        myHinge.useMotor = true;

    }

    void Awake()
    {
        CloseBook();
        journal = GetComponentInChildren<Journal>();
    }

    // Update is called once per frame
    public void OpenBook()
    {
        var motor = myHinge.motor;
        motor.targetVelocity = 200;
        myHinge.motor = motor;
        Debug.Log("motor should be true");
        StartCoroutine(ActivateJournalAfterSeconds(3));
    }
    public void CloseBook()
    {
        var motor = myHinge.motor;
        motor.targetVelocity = -200;
        myHinge.motor = motor;
        Debug.Log("motor should be false");
    }

    public IEnumerator ActivateJournalAfterSeconds(int time)
    {
        yield return new WaitForSeconds(time);

    }
}
