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
    ///��Ʈ�ڽ� ������
    ///</summary>
    public HitBox[] hitBoxPrefabs;
    /// <summary>
    /// ��Ʈ�ڽ� ���� ��ġ
    /// </summary>
    public Transform[] attackStartPos;
    ///<summary>
    ///������ ��Ʈ�ڽ��� ����Ǵ� ����
    ///</summary>
    public HitBox[] hitBoxes;
    ///<summary>
    ///���Ϳ��� �� �׼��� Ÿ�����ϴ� Ÿ�ٿ����� ����ũ
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
            //���� ���ư��� �������� Ÿ���� ��Ʈ�ڽ��� ��� ���ε�
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
