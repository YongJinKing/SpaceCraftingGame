using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBox : HitBox
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected bool _isFollowDir = false;
    protected Vector2 hitPosition = Vector2.zero;
    protected float offset;
    protected float angle;
    #endregion
    #region Public
    public bool isFollowDir
    {
        get { return _isFollowDir; }
        set { _isFollowDir = value; }
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
    protected override void HitCheckEnd()
    {
        OnDurationEndEvent?.Invoke();
        gameObject.SetActive(false);

        if (isFollowDir)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        base.HitCheckEnd();
    }
    protected override void Initialize()
    {
        base.Initialize();

        offset = hitBoxSize.x * 0.5f;
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        Deactivate();

        if (isFollowDir)
        {
            float dir = 1.0f;
            if (Vector2.Dot(transform.up, pos - (Vector2)transform.position) < 0.0f) dir = -1.0f;
            angle = Vector2.Angle(transform.right, pos - (Vector2)transform.position) * dir;
            transform.Rotate(Vector3.forward * angle, Space.World);

            hitPosition = new Vector2(transform.position.x + offset * Mathf.Cos(angle * Mathf.Deg2Rad), transform.position.y + offset * Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        base.Activate(pos);
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected override IEnumerator HitChecking()
    {
        float remainDuration = duration;
        
        while(remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;
            Collider2D[] tempcol;
            if (isCircle)
            {
                tempcol = Physics2D.OverlapCircleAll(hitPosition, hitBoxSize.x, targetMask);
            }
            else
            {
                tempcol = Physics2D.OverlapBoxAll(hitPosition, hitBoxSize, angle, targetMask);
            }

            for(int i = 0; i < tempcol.Length; ++i) 
            {
                Stat temp = tempcol[i].GetComponentInParent<Stat>();
                if(!calculatedObject.Contains(temp))
                {
                    //Debug
                    //Debug.Log(tempcol[i].gameObject.name);

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, tempcol[i].bounds.center, 10.0f, targetMask);
                    if(hit == default(RaycastHit2D))
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
    //for Debug
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (debuging)
        {
            //Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.1f);
            if (!isCircle)
                Gizmos.DrawCube(hitPosition, hitBoxSize);
            else
                Gizmos.DrawSphere(hitPosition, hitBoxSize.x * 0.5f);
        }
    }
#endif
    
    #endregion
}
