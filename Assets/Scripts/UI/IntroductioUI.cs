using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductioUI : SingleT<IntroductioUI>
{
    public GameObject introductionPrefab;
    public GameObject signPrefab;
    private bool isOpen=true;
    private void Update()
    {
        ClosedIntroduction();
    }
    public void ClosedIntroduction()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            isOpen = !isOpen;
            introductionPrefab.SetActive(isOpen);
            
        }
    }
}
