using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHitBox : HitBox
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Transform parent;
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
    protected override void HitCheckEnd()
    {
        OnDurationEndEvent?.Invoke();
        gameObject.SetActive(false);

        transform.SetParent(parent);
        base.HitCheckEnd();
    }
    protected override void Initialize()
    {
        parent = transform.parent;
        base.Initialize();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        transform.parent = null;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        base.Activate(pos);
    }
    public override void Deactivate()
    {
        base.Deactivate();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected override IEnumerator HitChecking()
    {
        float remainDuration = duration;

        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;
            Collider2D[] tempcol;
            if (isCircle)
            {
                tempcol = Physics2D.OverlapCircleAll(pos, hitBoxSize.x, targetMask);
            }
            else
            {
                tempcol = Physics2D.OverlapBoxAll(pos, hitBoxSize, 0.0f, targetMask);
            }

            for (int i = 0; i < tempcol.Length; ++i)
            {
                Stat temp = tempcol[i].GetComponentInParent<Stat>();
                if (!calculatedObject.Contains(temp))
                {
                    //Debug
                    //Debug.Log(tempcol[i].gameObject.name);

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, tempcol[i].bounds.center, 10.0f, targetMask);
                    if (hit == default(RaycastHit2D))
                    {
                        hit.point = transform.position;
                    }
                    HitEffectPlay(hit.point);

                    //Debug.Log(targetMask & (1 << tempcol[i].gameObject.layer));

                    calculatedObject.Add(temp);
                    onHitEvents[targetMask & (1 << tempcol[i].gameObject.layer)]?.Invoke(tempcol[i], hit.point);
                }
            }

            yield return null;
        }
        HitCheckEnd();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
