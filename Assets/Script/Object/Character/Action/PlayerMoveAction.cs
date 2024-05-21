using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : MoveAction
{
    #region Properties
    #region Private
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
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        moveToDirEvent?.Invoke(pos);
    }
    public override void Deactivate()
    {
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
