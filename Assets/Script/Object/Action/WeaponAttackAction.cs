using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//총 애니메이션과 캐릭터 애니메이션 컨트롤러의 분리가 필요하다.
//총을 들고있으면 캐릭터에서 팔을 빼야하기 때문이다. ㅠㅜㅠㅜ

//1. Action이 발동시 애니메이션 트리거
//2. 애니메이션 발동 도중 특정 애니메이션에서 이벤트 발생
//      예를 들면 총구가 번쩍 하는 순간에 트리거
//3. 이벤트 발생시 히트박스 생성
//4. 애니메이션에서 캔슬 가능한 영역에 도달시 이벤트 발생
//5. Action에 따라서 히트박스를 Deactivate 하거나 그냥 Action이 끝났다고 이벤트 발생
//6. 플레이어에게 다시 제어권이 돌아옴


//1-1. 애니메이션이 없는 경우 일정 시간이 지난후에 Action이 그냥 끝남

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
    /// 다른 동작으로 캔슬이 가능할때까지의 시간
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
    public override void OnHitBoxEnd()
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
