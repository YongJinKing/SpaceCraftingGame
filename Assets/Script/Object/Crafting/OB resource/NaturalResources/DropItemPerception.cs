using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPerception : MonoBehaviour
{
    public LayerMask layerMask;
    public float speed;
    float time;
    Transform parent;
    private void OnEnable()
    {
        parent = this.transform.parent;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            time += Time.deltaTime;

            if(time > 1f)
            {
                Vector2 dir = collision.transform.position - parent.transform.position;
                dir.Normalize();
                parent.transform.Translate(dir * speed * Time.deltaTime, Space.World);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            time = 0;
        }
    }
}
