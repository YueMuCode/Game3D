using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public KeyCode key;

    private SlotHolder slotHolder;
    private void Start()
    {
        slotHolder = GetComponent<SlotHolder>();
    }
    private void Update()
    {
        ButtonUseItem();
    }
    public void ButtonUseItem()
    {
        if(Input.GetKeyDown(key))
        {
            slotHolder.UseItem();
        }
    }
}
