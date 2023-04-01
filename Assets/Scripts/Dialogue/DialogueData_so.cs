using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Dialogue",menuName ="Dialogue")]
public class DialogueData_so : ScriptableObject
{
    public  List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();//��ζԻ��ܹ��ж��ٸ����
                                                                           //public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();//ID��Ӧ������

    //private void OnValidate()
    //{
    //    dialogueIndex.Clear();
    //    foreach(var piece in dialoguePieces)
    //    {
    //        if(!dialogueIndex.ContainsKey(piece.thisPieceID))
    //        {
    //            dialogueIndex.Add(piece.thisPieceID, piece);
    //        }
    //    }
    //}
    public QuestData_SO GetQuest()
    {
        QuestData_SO currentQuest = null;
        foreach (var piece in dialoguePieces)
        {
            if (piece.questdata != null)
            {
                currentQuest = piece.questdata;
            }
        }
        return currentQuest;
    }
  

}
