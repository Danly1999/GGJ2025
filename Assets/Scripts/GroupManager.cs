using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupManager : MonoBehaviour
{
    public GameObject end_image;
    public GameObject bb;
    AudioSource audio_source;
    public int nub;
    public float speed = 10;
    public int star_id = 0;
    public int end_id_1 = 1;
    public int end_id_2 = -1;
    public int end_id_3 = -1;
    GridLayoutGroup grid;
    public static int global_row;
    public static int global_column;
    static List<GameObject> bb_list = new List<GameObject>();
    public static List<KeyValuePair<int, int>> touchList = new List<KeyValuePair<int, int>>();
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // 删除子物体
        }
        audio_source = GetComponent<AudioSource>();
        bb_list = new List<GameObject>();
        StartCoroutine(WaitOneSecond());
        touchList = new List<KeyValuePair<int, int>>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.rect.width; // 获取宽度
        grid = GetComponent<GridLayoutGroup>();
        global_row = (int)(rectTransform.rect.width / grid.cellSize.x);
        global_column = (int)(nub / global_row);
        for (int i = 0; i < nub; i++)
        {
            GameObject newBB = Instantiate(bb);
            newBB.transform.SetParent(transform, false);
            Material mat = new Material(newBB.GetComponent<Image>().material);
            newBB.GetComponent<Image>().material = mat;
            bb_list.Add(newBB);
            BBManager bbm = newBB.GetComponent<BBManager>();
            bbm.column = (int)(i / global_row);
            bbm.row = i % global_row;
        }

    }
    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(0.01f); // 等待一秒
        {
            bb_list[star_id].GetComponent<BBManager>().SetValue(100);
            bb_list[star_id].GetComponent<Image>().material.SetFloat("_StaticTime", 0);
            bb_list[end_id_1]?.GetComponent<Image>().material.SetFloat("_IsEnd", 1);
            if (end_id_2 != -1)
            {
                bb_list[end_id_2]?.GetComponent<Image>().material.SetFloat("_IsEnd", 1);
            }
            if (end_id_3 != -1)
            {
                bb_list[end_id_3]?.GetComponent<Image>().material.SetFloat("_IsEnd", 1);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(end_image)
        {
            if(bb_list[end_id_1].GetComponent<BBManager>().v>=99.5)
            {
                end_image.SetActive(true);
            }
        }
        float a = 0;
        float b = 0;
        foreach (var rc in touchList)
        {
            foreach (var bb in bb_list)
            {

                if (bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column +1 == rc.Value && !bb.GetComponent<BBManager>().is_update)
                {
                    a++;
                }
                else if (bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column -1 == rc.Value && !bb.GetComponent<BBManager>().is_update)
                {
                    a++;
                }
                else if (bb.GetComponent<BBManager>().row + 1 == rc.Key && bb.GetComponent<BBManager>().column == rc.Value && !bb.GetComponent<BBManager>().is_update)
                {
                    a++;
                }
                else if (bb.GetComponent<BBManager>().row - 1 == rc.Key && bb.GetComponent<BBManager>().column == rc.Value && !bb.GetComponent<BBManager>().is_update)
                {
                    a++;
                }else if(bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column == rc.Value && bb.GetComponent<BBManager>().v > 0)
                {
                    b++;
                }
            }
        }
        if(a>0 && b>0)
        {
            if(!audio_source.isPlaying)
            {
                audio_source.UnPause();
            }
        }else
        {
            if (audio_source.isPlaying)
            {
                audio_source.Pause();
            }  
        }
        foreach (var rc in touchList)
        {
            foreach (var bb in bb_list)
            {

                if (bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column == rc.Value && a>0 && b > 0)
                {
                    bb.GetComponent<BBManager>().v -= 1 * Time.deltaTime * speed;
                }
                if (bb.GetComponent<BBManager>().is_update == true)
                {
                    continue;
                }

                if(bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column + 1 == rc.Value)
                {
                    bb.GetComponent<BBManager>().v += (float)(1.0 / (float)a * b)*Time.deltaTime* speed;
                }
                else if (bb.GetComponent<BBManager>().row == rc.Key && bb.GetComponent<BBManager>().column - 1 == rc.Value)
                {
                    bb.GetComponent<BBManager>().v += (float)(1.0 / (float)a * b) * Time.deltaTime * speed;
                }
                else if (bb.GetComponent<BBManager>().row + 1 == rc.Key && bb.GetComponent<BBManager>().column == rc.Value)
                {
                    bb.GetComponent<BBManager>().v += (float)(1.0 / (float)a * b) * Time.deltaTime * speed;
                }
                else if (bb.GetComponent<BBManager>().row - 1 == rc.Key && bb.GetComponent<BBManager>().column == rc.Value)
                {
                    bb.GetComponent<BBManager>().v += (float)(1.0 / (float)a * b) * Time.deltaTime * speed;
                }

            }
        }
    }
        
}
