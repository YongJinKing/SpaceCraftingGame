using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelectAI : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected LayerMask _targetMask;
    protected float _detectRadius = 5.0f;
    #endregion
    #region Public
    public LayerMask targetMask
    {
        get { return _targetMask; }
        protected set { _targetMask = value; }
    }
    public float detectRadius
    {
        get { return _detectRadius; }
        protected set { _detectRadius = value; }
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
    #endregion
    #region Public
    public GameObject Compute(LayerMask targetMask, float Radius)
    {
        this.targetMask = targetMask;
        this.detectRadius = Radius;

        //Scan Targets
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectRadius, targetMask);
        if( targets.Length <= 0 ) { return null; }

        int bestTarget = 0;
        float max = 0;

        IGetPriority getValue;
        float computedVal;
        for (int i = 0; i < targets.Length; ++i)
        {
            //Compute Best Target
            getValue = targets[i].GetComponentInParent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;
            
            if(max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
        }

        return targets[bestTarget].gameObject;
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
