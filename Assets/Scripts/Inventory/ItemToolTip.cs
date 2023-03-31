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
        UpdatePosition();//������ʾ�ĵ�һʱ��͸���һ������λ��
    }
    private void Update()
    {
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;//����ʾ���������ƶ�ֱ�������������ƶ��ᷢ��ͼƬ��ʾ�͵�ס���Ȼ��͹رգ��رպ������λ��ui�Ϸ�����ʾ�����Ի�һֱ��˸
        Vector3[] corners = new Vector3[4];//��¼��Ļ���ĸ����������
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
