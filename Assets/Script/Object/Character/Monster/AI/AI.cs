using UnityEngine;

public class AI : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected bool isWave;
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
    public GameObject TargetSelect(LayerMask targetMask, float Radius)
    {
        //Scan Targets
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, Radius, targetMask);
        if (targets.Length <= 0) { return null; }

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

            if (max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
        }

        getValue = targets[bestTarget].GetComponentInParent<IGetPriority>();
        //find target but, that can not become target
        if (getValue != null)
        {
            if (getValue.GetPriority() < 0)
            {
                return null;
            }
        }
        else { return null; }

        return targets[bestTarget].gameObject;
    }
    public Action SelectAction(Action[] actions)
    {
        int bestTarget = 0;
        float max = 0;

        IGetPriority getValue;
        float computedVal;

        for (int i = 0; i < actions.Length; ++i)
        {
            //Compute Best Target
            getValue = actions[i].GetComponent<IGetPriority>();
            if (getValue == null)
                continue;

            computedVal = getValue.GetPriority();
            if (computedVal < 0)
                continue;

            if (max < computedVal)
            {
                max = computedVal;
                bestTarget = i;
            }
        }

        return actions[bestTarget];
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
