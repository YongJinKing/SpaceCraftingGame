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
