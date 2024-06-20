using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossPerception : MonoBehaviour
{
    public List<GameObject> mortalBoxes = new List<GameObject>();
    public LayerMask mortalBoxLayerMask;

    private void Update()
    {
        this.transform.localPosition = Vector3.zero;
    }

    public List<GameObject> GetList() {
        return mortalBoxes; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & mortalBoxLayerMask) != 0)
        {
            mortalBoxes.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & mortalBoxLayerMask) != 0)
        {
            if (!mortalBoxes.Contains(collision.gameObject))
            {
                mortalBoxes.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & mortalBoxLayerMask) != 0)
        {
            mortalBoxes.Remove(collision.gameObject);
        }
    }
}
