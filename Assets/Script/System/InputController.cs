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
    public UnityEvent<Vector2> moveEvent = new UnityEvent<Vector2>();
    public UnityEvent<int, Vector2> mouseEvent = new UnityEvent<int, Vector2>();
    public UnityEvent<int> mouseUpEvent = new UnityEvent<int>();
    public UnityEvent<int> numberKeyEvent = new UnityEvent<int>();
    public UnityEvent<Vector2> getMousePosEvent = new UnityEvent<Vector2>();
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

        if (Input.GetMouseButton(0))
        {
            mouseEvent?.Invoke(0, mousePos);
        }
        if (Input.GetMouseButton(1))
        {
            mouseEvent?.Invoke(1, mousePos);
        }
        if (Input.GetMouseButton(2))
        {
            mouseEvent?.Invoke(2, mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpEvent?.Invoke(0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            mouseUpEvent?.Invoke(1);
        }
        if (Input.GetMouseButtonUp(2))
        {
            mouseUpEvent?.Invoke(2);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            numberKeyEvent?.Invoke(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            numberKeyEvent?.Invoke(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            numberKeyEvent?.Invoke(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            numberKeyEvent?.Invoke(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            numberKeyEvent?.Invoke(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            numberKeyEvent?.Invoke(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            numberKeyEvent?.Invoke(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            numberKeyEvent?.Invoke(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            numberKeyEvent?.Invoke(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            numberKeyEvent?.Invoke(0);
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