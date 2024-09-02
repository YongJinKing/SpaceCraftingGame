using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class WeaponAttackAction : AttackAction
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] float _activeDuration = 0.0f;
    #endregion
    #region Public
    /// <summary>
    /// �ٸ� �������� ĵ���� �����Ҷ������� �ð�
    /// </summary>
    public float activeDuration
    {
        get { return _activeDuration; }
        set { _activeDuration = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    WeaponAttackAction() { fireAndForget = true; }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);

        ActivateHitBoxes(pos);
        StartCoroutine(ActiveDurationChecking());
    }
    #endregion
    #endregion

    #region EventHandlers
    protected override void OnHitBoxEnd()
    {
    }
    #endregion

    #region Coroutines
    protected IEnumerator ActiveDurationChecking()
    {
        if(activeDuration > 0.0f)
            yield return new WaitForSeconds(activeDuration);
        ActionEnd();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
