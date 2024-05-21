using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent<Vector2> moveEvent;
    public UnityEvent<int, Vector2> mouseEvent;
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    private Vector2 GetMousePoint()
    {
        Debug.Log($"{Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    void Update()
    {
        moveEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        if (Input.GetMouseButtonDown(0))
        {
            mouseEvent?.Invoke(0, GetMousePoint());
        }
        if (Input.GetMouseButtonDown(1))
        {
            mouseEvent?.Invoke(1, GetMousePoint());
        }
        if (Input.GetMouseButtonDown(2))
        {
            mouseEvent?.Invoke(2, GetMousePoint());
        }
    }


    #endregion

    /*
    private void Start()
    {
        //For Debug
        moveEvent.AddListener(Fortest);
    }

    public void Fortest(Vector2 vec)
    {
        Debug.Log($"{Input.GetAxisRaw("Horizontal")}, {Input.GetAxisRaw("Vertical")}");
        Debug.Log($"{vec.x}, {vec.y}");
    }
    */
}