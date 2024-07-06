using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Player myPlayer;
    //triggered by left mouse click
    [SerializeField] protected Action _mainAction;
    //triggered by right mouse click
    [SerializeField] protected Action _subAction;
    [SerializeField] protected Transform _graphic;
    #endregion
    #region Public
    public Action mainAction
    {
        get { return _mainAction; }
        set { _mainAction = value; }
    }
    public Action subAction
    {
        get { return _subAction; }
        set { _subAction = value; }
    }
    public Transform graphic
    {
        get { return _graphic; }
        set { _graphic = value; }
    }
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
        myPlayer = GetComponentInParent<Player>();
        if(myPlayer != null)
        {
            graphic.SetParent(myPlayer.weaponRotationAxis.transform, false);
        }
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
        graphic.gameObject.SetActive(true);
        AddListeners();
    }
    public override void UnEquip()
    {
        base.UnEquip();
        graphic.gameObject.SetActive(false);
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
