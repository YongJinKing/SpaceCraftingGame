using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPosImage : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TurnOffImage());
    }

    IEnumerator TurnOffImage()
    {
        yield return new WaitForSeconds(2.5f);
        this.gameObject.SetActive(false);
    }
}
