using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DialogueUIController : SingleT<DialogueUIController>
{
    public Image imagePrefab;
    public Text mainTextPrefab;
    public Button nextButtonPrefab;
    public GameObject dialoguePanelPrefab;
    public DialogueData_so currentDialogueData;
    public int currentIndex = 0;//当前播放到那一段对话了

    [Header("OptionsButton")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;



    protected override void Awake()
    {
        base.Awake();
        nextButtonPrefab.onClick.AddListener(ContinueDialogue);//鼠标一直在监听事件
    }

    void ContinueDialogue()
    {
        if(currentIndex<currentDialogueData.dialoguePieces.Count)
        {
            UpdateCurrentPiece(currentDialogueData.dialoguePieces[currentIndex]);
        }
        else
        {
            dialoguePanelPrefab.gameObject.SetActive(false);
        }
        
    }

    public void UpdateDialogueData(DialogueData_so data)
    {
        currentDialogueData = data;
        currentIndex = 0;
    }
    public void UpdateCurrentPiece(DialoguePiece piece)//更新每一个语句
    {
        dialoguePanelPrefab.SetActive(true);//先启动对话的ui面板        
        currentIndex++;
       // Debug.Log(currentIndex);
        if (piece.speakerImage!=null)//如果这句话有图片
        {
            imagePrefab.enabled = true;
            imagePrefab.sprite = piece.speakerImage;
        }
        else
        {
            imagePrefab.enabled = false;
        }
        mainTextPrefab.text = "";//第一次打开先清空对话的内容
      //  mainTextPrefab.text = piece.text;//然后加载对话的内容
        mainTextPrefab.DOText(piece.text, 1.5f);

        if(piece.optionsOfthisPiece.Count==0&&currentDialogueData.dialoguePieces.Count>0)//控制对话右下角的next按钮
        {
            nextButtonPrefab.interactable = true;
            nextButtonPrefab.gameObject.SetActive(true);
            nextButtonPrefab.transform.GetChild(0).gameObject.SetActive(true);
            
           
        }
        else
        {
            nextButtonPrefab.interactable = false;
            nextButtonPrefab.gameObject.SetActive(true);
            nextButtonPrefab.transform.GetChild(0).gameObject.SetActive(false);
        }

        //创建这句话下面附带的回答选项按钮

        CreateOptions(piece);

    }



    void CreateOptions(DialoguePiece piece)
    {
        if(optionPanel.childCount>0)//panel地下已经有button了，那就销毁它
        {
            for(int i=0;i<optionPanel.childCount;i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);//销毁全部
            }
        }
        //销毁完了之后开始生成
        for(int i=0;i<piece.optionsOfthisPiece.Count;i++)//生成每一句话附带的按钮
        {
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece, piece.optionsOfthisPiece[i]); 
        }
    }
}
