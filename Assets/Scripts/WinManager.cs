using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject[] groups;
    public GameObject[] bbs;
    public GameObject Trans;
    public GameObject Tips;
    AudioSource audio_source;
    public AudioClip clip;
    int id;
    public int bbid;
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Win()
    {
        Tips.SetActive(false);
        groups[id].SetActive(false);
        if(id +1 >= groups.Length)
        {
            id = 0;
            bbid++;
            if (bbid  >= bbs.Length)
            {
                bbid = 0;
            }
            
            foreach (var b in groups)
            {
                b.GetComponent<GroupManager>().bb = bbs[bbid];
            }
        }
        else
        {
            id++;
        }
        groups[id].SetActive(true);
    }
    public void ReOpen()
    {
        StartCoroutine(WaitOneSecond(id));
    }
    public void SelectSence(int s_id)
    {
        StartCoroutine(WaitOneSecond(s_id));
    }
    private IEnumerator WaitOneSecond(int s_id)
    {
        audio_source.PlayOneShot(clip, 1);
        Trans.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        groups[id].SetActive(false);
        id = s_id;
        groups[id].SetActive(true);
    }
}
