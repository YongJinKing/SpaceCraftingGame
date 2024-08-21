using UnityEngine;
using UnityEngine.Events;

public class PlayerBuildModeState : PlayerState
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    private UnityEvent<Vector2> moveEvent = new UnityEvent<Vector2>();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        InputController.Instance.moveEvent.AddListener(OnMove);
        InputController.Instance.keyEvent.AddListener(OnKeyDown);

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.AddListener(unitMovement.OnMoveToDir);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.moveEvent.RemoveListener(OnMove);
        InputController.Instance.keyEvent.RemoveListener(OnKeyDown);

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.RemoveListener(unitMovement.OnMoveToDir);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        moveEvent?.Invoke(dir);

        owner.myAnim.SetMove(true);

        if (Mathf.Approximately(dir.x, 0.0f) && Mathf.Approximately(dir.y, 0.0f))
            owner.myAnim.SetMove(false);
    }

    public void OnKeyDown(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.B:
                {
                    owner.stateMachine.ChangeState<PlayerIdleState>();
                }
                break;
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
