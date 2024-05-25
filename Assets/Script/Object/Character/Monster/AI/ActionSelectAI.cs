using UnityEngine;

public class ActionSelectAI : MonoBehaviour
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
    #endregion
    #region Public
    public Action Compute(Action[] actions)
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
