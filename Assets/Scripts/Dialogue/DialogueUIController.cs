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
    public int currentIndex = 0;//��ǰ���ŵ���һ�ζԻ���

    [Header("OptionsButton")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;



    protected override void Awake()
    {
        base.Awake();
        nextButtonPrefab.onClick.AddListener(ContinueDialogue);//���һֱ�ڼ����¼�
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
    public void UpdateCurrentPiece(DialoguePiece piece)//����ÿһ�����
    {
        dialoguePanelPrefab.SetActive(true);//�������Ի���ui���        
        currentIndex++;
       // Debug.Log(currentIndex);
        if (piece.speakerImage!=null)//�����仰��ͼƬ
        {
            imagePrefab.enabled = true;
            imagePrefab.sprite = piece.speakerImage;
        }
        else
        {
            imagePrefab.enabled = false;
        }
        mainTextPrefab.text = "";//��һ�δ�����նԻ�������
      //  mainTextPrefab.text = piece.text;//Ȼ����ضԻ�������
        mainTextPrefab.DOText(piece.text, 1.5f);

        if(piece.optionsOfthisPiece.Count==0&&currentDialogueData.dialoguePieces.Count>0)//���ƶԻ����½ǵ�next��ť
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

        //������仰���渽���Ļش�ѡ�ť

        CreateOptions(piece);

    }



    void CreateOptions(DialoguePiece piece)
    {
        if(optionPanel.childCount>0)//panel�����Ѿ���button�ˣ��Ǿ�������
        {
            for(int i=0;i<optionPanel.childCount;i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);//����ȫ��
            }
        }
        //��������֮��ʼ����
        for(int i=0;i<piece.optionsOfthisPiece.Count;i++)//����ÿһ�仰�����İ�ť
        {
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece, piece.optionsOfthisPiece[i]); 
        }
    }
}
