using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuspectPicking : MonoBehaviour
{
    public List<bool> CharacterList = new List<bool>();
    public List<GameObject> Characters = new List<GameObject>();
    public List<GameObject> TpLocations = new List<GameObject>();
    public List<GameObject> Object = new List<GameObject>();
    public List<GameObject> TpOffice = new List<GameObject>();
    public List<GameObject> convictBoxes = new List<GameObject>();
    public FadeScreen fadeScreen;
    public int hiddenDialogueUnlockCount;

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

    public void LorettaToggleAccused()
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

    public void ManyelToggleAccused()
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

    public void RicardoToggleAccused()
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


    public void InitiateTP()
    {
        Debug.Log("InitiateTP");
        fadeScreen.FadeOut();
        StartCoroutine(WaitForFadeScreen());
    }
    public void InitiateOffice()
    {
        Debug.Log("InitiateOffice");
        fadeScreen.FadeOut();
        StartCoroutine(WaitForFadeScreenOffice());
    }
    public IEnumerator WaitForFadeScreen()
    {
        yield return new WaitForSeconds(2f);
        NPCTP();
    }
    public void NPCTP()
    {
        for (int i = 0; i < 8; i++)
        {
            Characters[i].GetComponent<SphereCollider>().enabled = false;
            Characters[i].transform.position = TpLocations[i].transform.position;
            Characters[i].transform.localRotation = TpLocations[i].transform.localRotation;
            convictBoxes[i].SetActive(true);
            fadeScreen.FadeIn();
        }
    }
    public IEnumerator WaitForFadeScreenOffice()
    {
        yield return new WaitForSeconds(2f);
        TpToOffice();
    }
    public void TryTPToOffice()
    {
        foreach (bool suspect in CharacterList)
        {
            if (suspect == true)
            {
                InitiateOffice();
                return;
            }
        }
    }

    public void TpToOffice()
    {
        var allPersons = FindObjectsOfType<Person>().ToList();
        for (int i = 0; i < allPersons.Count - 1; i++)
        {
            hiddenDialogueUnlockCount += allPersons[i].testimonyCount;
        }
        for (int i = 0; i < 2; i++)
        {
            Object[i].transform.position = TpOffice[i].transform.position;
            Object[i].transform.localRotation = TpOffice[i].transform.localRotation;
        }
        fadeScreen.FadeIn();
    }
}
