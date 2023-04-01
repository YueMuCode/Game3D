using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestRequirment : MonoBehaviour
{
    private Text requireName;
    private Text progressNumber;
    void Awake()
    {
        requireName = GetComponent<Text>();
        progressNumber = transform.GetChild(0).GetComponent<Text>();
    }
    public void SetupRequirement(string name, int amount, int currentAmount)
    {
        requireName.text = name;
        progressNumber.text = currentAmount.ToString() + "/" + amount.ToString();
    }
    public void SetupRequiremennt(string name, bool isFinished)
    {
        if (isFinished)
        {
            requireName.text = name;
            progressNumber.text = "���";
            requireName.color = Color.gray;
            progressNumber.color = Color.gray;
        }
    }
}
