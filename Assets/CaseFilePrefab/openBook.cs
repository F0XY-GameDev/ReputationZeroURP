using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openBook : MonoBehaviour
{
    public GameObject Cover;
    public HingeJoint myHinge;
    [SerializeField] GameObject TitleCanvas;
    
    void Start()
    {
        var myHinge = Cover.GetComponent<HingeJoint>();
    }

    void Awake()
    {        
    }

    // Update is called once per frame
    public void OpenBook()
    {
        myHinge.useMotor = true;
        var motor = myHinge.motor;
        motor.targetVelocity = 200;
        myHinge.motor = motor;
        Debug.Log("motor should be true");
        StartCoroutine(ActivateJournalAfterSeconds(0.6f));
    }
    public void CloseBook()
    {
        var motor = myHinge.motor;
        motor.targetVelocity = -200;
        myHinge.motor = motor;
        Debug.Log("motor should be false");
        
    }

    public IEnumerator ActivateJournalAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        TitleCanvas.SetActive(true);
    }

    public IEnumerator DeactivateMotor()
    {
        yield return new WaitForSeconds(3);
        myHinge.useMotor = false;
    }
}
