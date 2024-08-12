using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodlebugDetectPerception : MonoBehaviour
{
    public Doodlebug doodleBug;
    public LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            doodleBug.SetTarget(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            doodleBug.MissTarget();
        }
    }
}
