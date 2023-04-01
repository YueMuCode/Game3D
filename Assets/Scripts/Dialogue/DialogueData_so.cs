using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Dialogue",menuName ="Dialogue")]
public class DialogueData_so : ScriptableObject
{
   public  List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();//这段对话总共有多少哥语句
}
