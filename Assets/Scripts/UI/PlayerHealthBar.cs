using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    public Transform canvas;
    public Transform healthSlider;
    public Transform expSlider;
    public Transform text;
    PlayerController player;
    public void Start()
    {

        //if (GetComponent<PlayerController>().characterStats)
        //{
        //    Debug.Log("拿到了");
        //}
        //else
        //    Debug.Log("拿不到");
        player = GetComponent<PlayerController>();
        InitTransform();
    }
    void InitTransform()
    {
        GameObject canvasTemp = GameObject.Find("PlayerHealthCanvas");
        if(canvasTemp)
        {
            canvas = canvasTemp.transform;
            healthSlider = canvas.GetChild(0);
            expSlider = canvas.GetChild(1);
            text = canvas.GetChild(2);
         //   healthSlider.GetComponent<Slider>().maxValue = player.characterStats.maxHealth;



        }
        else
        {
            Debug.Log("没有找到这个PlayerHealthCanvas");
        }
        GetComponent<PlayerController>().UpdatePlayerHealth += UpdatePlayerHealthUI;
        GetComponent<PlayerController>().UpdatePlayerHealth += UpdatePlayerExpUI;
        GetComponent<PlayerController>().UpdatePlayerHealth += UpdateLevelTextUI;
    }

    void UpdatePlayerHealthUI(int a)
    {
        healthSlider.GetComponent<Slider>().maxValue = player.characterStats.maxHealth;
        healthSlider.GetComponent<Slider>().value = GetComponent<PlayerController>().characterStats.currentHealth;
    }

    void UpdatePlayerExpUI(int a)
    {
        expSlider.GetComponent<Slider>().maxValue = player.characterStats.needExp;
        expSlider.GetComponent<Slider>().value = player.characterStats.currentExp;
    }
    void UpdateLevelTextUI(int a)
    {
        text.GetComponent<Text>().text = "LV" + player.characterStats.level.ToString("00");
    }

}
