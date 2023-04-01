using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialoguePiece
{
    public int thisPieceID;//这个语句对应的id
    public Sprite speakerImage;//说话者的图片

    [TextArea]
    public string text;//说话的内容

    public List<DialogueOptions> optionsOfthisPiece = new List<DialogueOptions>();

}

