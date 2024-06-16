using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGround : MonoBehaviour
{
    public LayerMask layerMask;
    float originSpeed;
    Unit player;
    bool slowed;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<Unit>();
        originSpeed = player.moveSpeed;
    }
    void OnEnable()
    {
        Destroy(gameObject, 5f);
        slowed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            Debug.Log(collision.name);
            if (!slowed)
            {
                slowed = true;
                collision.gameObject.GetComponent<Unit>().moveSpeed *= 0.8f;
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            Debug.Log(collision.name);
            
            if (!slowed)
            {
                slowed = true;
                collision.gameObject.GetComponent<Unit>().moveSpeed *= 0.8f;
            }
            /*originSpeed = collision.gameObject.GetComponent<Unit>().moveSpeed;
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            slowed = false;
            collision.gameObject.GetComponent<Unit>().moveSpeed = originSpeed;
        }
    }

    private void OnDestroy()
    {
        player.moveSpeed = originSpeed;
    }
}
