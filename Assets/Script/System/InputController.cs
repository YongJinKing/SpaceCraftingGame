using UnityEngine;
using UnityEngine.Events;

public class InputController : Singleton<InputController>
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
    public UnityEvent<Vector2> getMousePosEvent;
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    private Vector2 GetMousePoint()
    {
        //Debug.Log($"{Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
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
        Vector2 mousePos = GetMousePoint();

        moveEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        getMousePosEvent?.Invoke(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            mouseEvent?.Invoke(0, mousePos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            mouseEvent?.Invoke(1, mousePos);
        }
        if (Input.GetMouseButtonDown(2))
        {
            mouseEvent?.Invoke(2, mousePos);
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