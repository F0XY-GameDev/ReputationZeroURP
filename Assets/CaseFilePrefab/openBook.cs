using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openBook : MonoBehaviour
{
    public GameObject Cover;
    public HingeJoint myHinge;
    
    void Start()
    {
        var myHinge = Cover.GetComponent<HingeJoint>();
        myHinge.useMotor = true;

    }

    // Update is called once per frame
    public void OpenBook()
    {
        var motor = myHinge.motor;
        motor.targetVelocity = 200;
        myHinge.motor = motor;
        Debug.Log("motor should be true");
    }
    public void CloseBook()
    {
        var motor = myHinge.motor;
        motor.targetVelocity = -200;
        myHinge.motor = motor;
        Debug.Log("motor should be false");
    }

}
