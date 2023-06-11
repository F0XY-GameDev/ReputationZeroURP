using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TextCore.Text;

public class SuspectPicking : MonoBehaviour
{
    public List<bool> CharacterList = new List<bool>();
    public List<GameObject> Characters = new List<GameObject>();
    public List<GameObject> TpLocations = new List<GameObject>();

    public void AlexToggleAccused()
    {
        if (CharacterList[0] == true)
        {
            CharacterList[0] = false;
            return;
        }
        
        if (CharacterList[0] == false)
        {
            CharacterList[0] = true;
            return;
        }
    }

    public void JamesToggleAccused()
    {
        if (CharacterList[1] == true)
        {
            CharacterList[1] = false;
            return;
        }

        if (CharacterList[1] == false)
        {
            CharacterList[1] = true;
            return;
        }
    }

    public void JannaToggleAccused()
    {
        if (CharacterList[2] == true)
        {
            CharacterList[2] = false;
            return;
        }

        if (CharacterList[2] == false)
        {
            CharacterList[2] = true;
            return;
        }
    }

    public void LorettaToggleAccused()
    {
        if (CharacterList[3] == true)
        {
            CharacterList[3] = false;
            return;
        }

        if (CharacterList[3] == false)
        {
            CharacterList[3] = true;
            return;
        }
    }

    public void ManyelToggleAccused()
    {
        if (CharacterList[4] == true)
        {
            CharacterList[4] = false;
            return;
        }

        if (CharacterList[4] == false)
        {
            CharacterList[4] = true;
            return;
        }
    }

    public void MelissaToggleAccused()
    {
        if (CharacterList[5] == true)
        {
            CharacterList[5] = false;
            return;
        }

        if (CharacterList[5] == false)
        {
            CharacterList[5] = true;
            return;
        }
    }

    public void RicardoToggleAccused()
    {
        if (CharacterList[6] == true)
        {
            CharacterList[6] = false;
            return;
        }

        if (CharacterList[6] == false)
        {
            CharacterList[6] = true;
            return;
        }
    }

    public void RowanToggleAccused()
    {
        if (CharacterList[7] == true)
        {
            CharacterList[7] = false;
            return;
        }

        if (CharacterList[7] == false)
        {
            CharacterList[7] = true;
            return;
        }
    }

    
    public void NPCTP()
    {
        for (int i = 0; i < 8; i++)
        {
            Characters[i].transform.localRotation = TpLocations[i].transform.localRotation;
        }
    }
}
