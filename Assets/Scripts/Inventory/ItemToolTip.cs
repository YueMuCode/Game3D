using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemToolTip : MonoBehaviour
{

    public Text itemName;
    public Text itemDescription;
    RectTransform rectTransform;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetItemText(ItemData_SO itemdata)
    {
        itemName.text = itemdata.itemName;
        itemDescription.text = itemdata.description;
    }
    private void OnEnable()
    {
        UpdatePosition();//在面显示的第一时间就更新一下它的位置
    }
    private void Update()
    {
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;//让提示版跟随鼠标移动直接让其跟着鼠标移动会发生图片显示就挡住鼠标然后就关闭，关闭后鼠标又位于ui上方又显示，所以会一直闪烁
        Vector3[] corners = new Vector3[4];//记录屏幕的四个角落的坐标
        rectTransform.GetWorldCorners(corners);
        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;
        if(mousePos.y<height)
        {
            rectTransform.position = mousePos + Vector3.up * height * 0.6f;
        }
        else if(Screen.width-mousePos.x>width)
        {
            rectTransform.position = mousePos + Vector3.right * width * 0.6f;
        }else
        {
            rectTransform.position = mousePos + Vector3.left * width * 0.6f;
        }
    }
}
