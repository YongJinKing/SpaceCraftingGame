using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    //triggered by left mouse click
    [SerializeField] protected Action mainAction;
    //triggered by right mouse click
    [SerializeField] protected Action subAction;
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
        
    }
    protected virtual void AddListeners()
    {

    }
    protected virtual void RemoveListeners()
    {

    }
    #endregion
    #region Public
    public override void Equip()
    {
        base.Equip();
        AddListeners();
    }
    public override void UnEquip()
    {
        base.UnEquip();
        RemoveListeners();
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
