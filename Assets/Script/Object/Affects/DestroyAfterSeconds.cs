using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;
    private void OnEnable()
    {
        Destroy(this.gameObject, seconds);
    }
}
