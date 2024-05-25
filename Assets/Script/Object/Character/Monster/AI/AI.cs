using UnityEngine;

public class AI : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected Monster owner;
    [SerializeField]protected TargetSelectAI targetSelectAI;
    [SerializeField]protected ActionSelectAI actionSelectAI;
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
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected void Start()
    {
        if(targetSelectAI == null)
            targetSelectAI = GetComponentInChildren<TargetSelectAI>();
        if(actionSelectAI == null)
            actionSelectAI = GetComponentInChildren<ActionSelectAI>();
        if (owner == null)
            owner = GetComponentInParent<Monster>();
    }
    #endregion
}
