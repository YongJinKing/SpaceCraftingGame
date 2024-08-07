using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected bool isEquiped = false;
    [SerializeField] protected EEquipmentType _itemType;
    [SerializeField] protected int _animType;
    #endregion
    #region Public
    public EEquipmentType itemType
    {
        get { return _itemType; }
        set { _itemType = value; }
    }
    public int animType
    {
        get { return _animType; }
        set { _animType = value; }
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
    protected abstract void Initialize();
    #endregion
    #region Public
    public virtual void Equip()
    {
        if (isEquiped)
            return;

        gameObject.SetActive(true);
        isEquiped = true;
    }
    public virtual void UnEquip()
    {
        if (!isEquiped)
            return;

        gameObject.SetActive(false);
        isEquiped = false;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected void Start()
    {
        Initialize();
    }
    #endregion
}
