using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BBManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI text;
    public bool is_update;
    public int row;
    public int column;
    public GameObject childObject;
    private float _v;
    public float v
    {
        get { return _v; }
        set 
        {   text.text = $"{(int)value}%";
            if(value>1)
            {
                childObject.SetActive(true);
            }
            else
            {
                childObject.SetActive(false);
            }
            _v = Mathf.Clamp(value, 0,100) ; 
            GetComponent<Image>().material.SetFloat("_StaticTime", Mathf.Clamp((1 - (value / 100))*10-1,0,9));
        }
    }
    private void OnEnable()
    {
        Button button = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if(v<=0)
        {
            for (int k = GroupManager.touchList.Count - 1; k >= 0; k--)
            {
                if (GroupManager.touchList[k].Key == row && GroupManager.touchList[k].Value == column)
                {
                    GroupManager.touchList.RemoveAt(k);
                }
            }
        }

    }

    public void SetValue(int targetInt)
    {
        v = targetInt;
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        var pair = new KeyValuePair<int, int>(row, column);

        GroupManager.touchList.Add(pair);

        is_update = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        for (int k = GroupManager.touchList.Count - 1; k >= 0; k--)
        {
            if (GroupManager.touchList[k].Key == row && GroupManager.touchList[k].Value == column)
            {
                GroupManager.touchList.RemoveAt(k);
            }
        }
        is_update = false;

    }
}
