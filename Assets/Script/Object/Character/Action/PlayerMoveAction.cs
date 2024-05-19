using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : MoveAction
{
    #region Properties
    #region Private
    //for debug, Serialize
    [SerializeField]private bool activated = false;
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        UnitMovement movement = GetComponentInParent<UnitMovement>();
        moveToPosEvent.AddListener(movement.OnMoveToPos);
        moveToDirEvent.AddListener(movement.OnMoveToDir);

        InputController inputController = GameObject.Find("InputController").GetComponent<InputController>();
        inputController.moveEvent.AddListener(OnMove);
    }
    #endregion
    #region Public
    public override void Activate()
    {
        activated = true;
    }
    public override void Deactivate()
    {
        activated &= false;
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        if(activated)
        {
            moveToDirEvent?.Invoke(dir);
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
