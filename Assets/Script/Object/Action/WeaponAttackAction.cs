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
    [SerializeField] protected int componentIndex;
    [SerializeField] protected int animationType;
    [SerializeField] protected float actionDuration;
    /// <summary>
    /// 만약 componentIndex 나 animationType 둘중 하나라도 음수이면 false
    /// </summary>
    protected bool isAnimationed
    {
        get { return componentIndex > 0 || animationType > 0; }
    }
    #endregion
    #region Public
    #endregion
    #region Events
    /// <summary>
    /// 애니메이션을 트리거 해주는 이벤트
    /// 첫 int는 컴포넌트 index, 두번째 int는 애니매이션 번호
    /// </summary>
    public UnityEvent<int, int> animationTriggerEvent = new UnityEvent<int, int>();
    public UnityEvent animationEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    WeaponAttackAction() { fireAndForget = true; }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        //플레이어의 애니메이션과 총의 애니메이션이 분리 되어 있어야함
        //
        //여기에 애니메이션 컨트롤러의 이벤트에 이 클래스의 핸들러를 등록
        //또한 이 클래스의 이벤트도 애니메이션 컨트롤러의 핸들러를 등록
        //애니메이션이 필요하지 않으면 등록하지 않음
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);

        if (!isAnimationed) 
        {
            //애니메이션으로 제어가 되지 않는경우(애니메이션이 없는경우)
            ActivateHitBoxes(pos);
            //여기에 총을 회전시키는 함수
            //여기에 총의 스프라이트를 비활성화는 함수
            //여기에 
        }
        else
        {
            animationTriggerEvent?.Invoke(componentIndex, animationType);
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    public override void OnHitBoxEnd()
    {
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
