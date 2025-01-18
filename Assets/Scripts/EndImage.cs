using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndImage : MonoBehaviour
{
    public WinManager winManager;
    public GameObject trans;
    private void OnEnable()
    {
        StartCoroutine(WaitOneSecond());
    }
    private void OnDisable()
    {
        
    }
    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(0.5f);
        trans.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        winManager.Win();
        gameObject.SetActive(false);
        

    }
}
