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
        if(myPlayer == null)
            myPlayer = GetComponentInParent<Player>();

        if(myPlayer != null && graphic != null)
        {
            graphic.SetParent(myPlayer.weaponRotationAxis.transform, false);
            graphic.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
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
        if(graphic != null)
            graphic.gameObject.SetActive(true);

        myPlayer.mainAction = this.mainAction;
        myPlayer.secondAction = this.subAction;

        AddListeners();
    }
    public override void UnEquip()
    {
        base.UnEquip();
        if (graphic != null)
            graphic.gameObject.SetActive(false);

        myPlayer.mainAction = null;
        myPlayer.secondAction = null;

        RemoveListeners();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void OnDestroy()
    {
        if (graphic != null) 
        { 
            Destroy(graphic.gameObject);
        }
    }
    #endregion
}
