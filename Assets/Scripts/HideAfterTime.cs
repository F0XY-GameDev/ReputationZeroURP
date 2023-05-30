using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI tmpugui;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAway());
    }

    public void SetText(EvidenceData evidenceData)
    {
        tmpugui.text = evidenceData.ItemName + " has been added to the Journal";
    }
    
    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
}
