using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected float ATK;
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Monster() : base() 
    {
        AddStat(EStat.ATK, ATK);
        float a = this[EStat.ATK];
    }
    #endregion

    #region Methods
    #region Private
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
    #endregion
}
