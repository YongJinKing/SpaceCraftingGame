using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField]
    private bool x, y;
    [SerializeField]
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(!target) return;

        transform.position = new Vector2(
            (x ? target.position.x : transform.position.x),
            (y ? target.position.y : transform.position.y));
    }
}
