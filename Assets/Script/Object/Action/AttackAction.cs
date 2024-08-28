using UnityEngine;
using UnityEngine.Events;

public abstract class AttackAction : Action
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField]protected LayerMask _targetMask;
    #endregion
    #region Public
    ///<summary>
    ///히트박스 프리펩
    ///</summary>
    public HitBox[] hitBoxPrefabs;
    /// <summary>
    /// 히트박스 시작 위치
    /// </summary>
    public Transform[] attackStartPos;
    ///<summary>
    ///생성된 히트박스가 저장되는 공간
    ///</summary>
    public HitBox[] hitBoxes;
    ///<summary>
    ///몬스터에서 이 액션이 타겟팅하는 타겟에대한 마스크
    ///</summary>
    public LayerMask targetMask
    {
        get { return _targetMask; }
        set { _targetMask = value; }
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
    protected virtual void ActivateHitBoxes(Vector2 pos)
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
            //만약 날아가서 없어지는 타입의 히트박스의 경우 리로드
            if (hitBoxes[i].isDestroy)
            {
                if(i < attackStartPos.Length && attackStartPos[i] != null)
                    hitBoxes[i] = Instantiate(hitBoxPrefabs[i], attackStartPos[i], false);
                else
                    hitBoxes[i] = Instantiate(hitBoxPrefabs[i], this.transform, false);

                hitBoxes[i].gameObject.SetActive(true);
            }
        }
    }

    protected override void Initialize()
    {
        if(hitBoxPrefabs != null)
        {
            hitBoxes = new HitBox[hitBoxPrefabs.Length];
            for(int i = 0; i < hitBoxes.Length; ++i)
            {
                if (i < attackStartPos.Length && attackStartPos[i] != null)
                    hitBoxes[i] = Instantiate(hitBoxPrefabs[i], attackStartPos[i], false);
                else
                    hitBoxes[i] = Instantiate(hitBoxPrefabs[i], this.transform, false);

                hitBoxes[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            if (!hitBoxes[i].isDestroy)
            {
                hitBoxes[i].OnDurationEndEvent.AddListener(OnHitBoxEnd);
            }   
        }
    }
    #endregion
    #region Public
    public override void Deactivate()
    {
        for(int i = 0; i < hitBoxes.Length; ++i)
        {
            if (!hitBoxes[i].isDestroy)
            {
                hitBoxes[i].Deactivate();
            }
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    public virtual void OnHitBoxEnd()
    {
        ActionEnd();
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void OnDestroy()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            if (!hitBoxes[i].isDestroy)
                hitBoxes[i].OnDurationEndEvent.RemoveListener(OnHitBoxEnd);
        }
    }
    #endregion
}
