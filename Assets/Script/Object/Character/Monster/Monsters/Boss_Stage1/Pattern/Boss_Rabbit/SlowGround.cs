using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGround : MonoBehaviour
{
    public LayerMask layerMask;
    float originSpeed;
    float slowSpeed;
    UnitMovement player;
    bool slowed;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    void OnEnable()
    {
        player = FindObjectOfType<UnitMovement>();
        originSpeed = player.GetSpeed();
        slowSpeed = originSpeed * 0.7f;
        Destroy(gameObject, 5f);
        slowed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            
            if (!slowed)
            {
                slowed = true;
                collision.gameObject.GetComponent<UnitMovement>().OnMoveSpeedStatChanged(originSpeed, slowSpeed);
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            
            if (!slowed)
            {
                slowed = true;
                collision.gameObject.GetComponent<UnitMovement>().OnMoveSpeedStatChanged(originSpeed, slowSpeed);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            slowed = false;
            collision.gameObject.GetComponent<UnitMovement>().OnMoveSpeedStatChanged(collision.gameObject.GetComponent<UnitMovement>().GetSpeed(),originSpeed);
        }
    }

    private void OnDestroy()
    {
        player.OnMoveSpeedStatChanged(player.GetSpeed(), originSpeed);
    }
}
