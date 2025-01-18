using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransEnd : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(WaitOneSecond());
    }
    private void OnDisable()
    {

    }
    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);


    }
}
