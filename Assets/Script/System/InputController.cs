using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityEvent<Vector2Int> moveEvent;
    public UnityEvent<int> mouseEvent;

    // Update is called once per frame
    void Update()
    {
        moveEvent?.Invoke(new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical")));

        if(Input.GetMouseButtonDown(0))
        {
            mouseEvent?.Invoke(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            mouseEvent?.Invoke(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            mouseEvent?.Invoke(2);
        }
    }
}