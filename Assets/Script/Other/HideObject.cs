using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] float timeHide;
    Coroutine coroutine;
    private void OnEnable()
    {
        coroutine = null;
        coroutine = StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(timeHide);
        gameObject.SetActive(false);
    }
}
