using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitBox : HitBox
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _maxDist;
    protected Transform parent;
    #endregion
    #region Public
    public float moveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    public float maxDist
    {
        get { return _maxDist; }
        set { _maxDist = value; }
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
        transform.rotation = Quaternion.identity;
        base.Activate(pos);
        StartCoroutine(LinearMoving());
    }
    public override void Deactivate()
    {
        base.Deactivate();
        transform.SetParent(parent);
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected override IEnumerator HitChecking()
    {
        float remainDuration = duration;
        this.pos.Normalize();


        while (remainDuration >= 0.0f)
        {
            remainDuration -= Time.deltaTime;
            Collider2D[] tempcol;
            if (isCircle)
            {
                tempcol = Physics2D.OverlapCircleAll((Vector2)transform.position, hitBoxSize.x, targetMask);
            }
            else
            {
                tempcol = Physics2D.OverlapBoxAll((Vector2)transform.position, hitBoxSize, transform.rotation.eulerAngles.z, targetMask);
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
        OnDurationEndEvent?.Invoke();
        gameObject.SetActive(false);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.SetParent(parent);

        Refresh();
    }
    protected IEnumerator LinearMoving()
    {
        float delta = 0.0f;
        float dist = maxDist;

        Vector2 dir;
        if (gameObject.activeSelf)
        {
            dir = pos - (Vector2)transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
            dir.Normalize();

            while (gameObject.activeSelf && dist >= 0.0f)
            {
                delta = Time.deltaTime * moveSpeed;
                dist -= delta;

                transform.Translate(dir * delta, Space.World);

                yield return null;
            }
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
