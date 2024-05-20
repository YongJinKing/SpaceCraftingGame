using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBox : HitBox
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected float offset;
    protected float angle;
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
    protected override void Initialize()
    {
        base.Initialize();
        offset = hitBoxSize.x;
        transform.Translate(Vector3.right * offset, Space.World);
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public override void OnActivated(Vector2 pos)
    {
        float dir = 1.0f;
        if (Vector2.Dot(transform.up, pos - (Vector2)transform.position) < 0.0f) dir = -1.0f;
        angle = Vector2.Angle(transform.right, pos) * dir;
        transform.Rotate(Vector3.forward * angle, Space.World);
        base.OnActivated(pos);
    }
    #endregion

    #region Coroutines
    protected override IEnumerator HitChecking()
    {
        float remainDuration = duration;
        this.pos.Normalize();
        

        while(remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;
            Collider2D[] tempcol;
            if (isCircle)
            {
                tempcol = Physics2D.OverlapCircleAll((Vector2)transform.position, hitBoxSize.x, targetMask);
            }
            else
            {
                tempcol = Physics2D.OverlapBoxAll((Vector2)transform.position, hitBoxSize, angle, targetMask);
            }

            for(int i = 0; i < tempcol.Length; ++i) 
            {
                Stat temp = tempcol[i].GetComponentInParent<Stat>();
                if(!calculatedObject.Contains(temp))
                {
                    //Debug
                    Debug.Log(tempcol[i].gameObject.name);

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, tempcol[i].bounds.center, 10.0f, targetMask);
                    if(hit == default(RaycastHit2D))
                    {
                        hit.point = transform.position;
                    }
                    HitEffectPlay(hit.point);

                    calculatedObject.Add(temp);
                    onHitEvents[targetMask | (1 << tempcol[i].gameObject.layer)]?.Invoke(tempcol[i], hit.point);
                }
            }

            yield return null;
        }
        gameObject.SetActive(false);
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
