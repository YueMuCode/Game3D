using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialoguePiece
{
    public int thisPieceID;//�������Ӧ��id
    public Sprite speakerImage;//˵���ߵ�ͼƬ

    [TextArea]
    public string text;//˵��������

    public List<DialogueOptions> optionsOfthisPiece = new List<DialogueOptions>();

}

