using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SetMovementType : MonoBehaviour
{ 
    public TeleportationProvider teleportation;
    public ActionBasedContinuousMoveProvider continuousMove;

    public void SetTypeFromIndex(int index)
    {
        if (index == 0)
        {
            teleportation.enabled = false;
            continuousMove.enabled = true;
        }
        else if (index == 1)
        {
            teleportation.enabled = true;
            continuousMove.enabled = false;
        }
    }
}