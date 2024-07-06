using UnityEngine;

//플레이어는 공격 애니메이션을 트리거하지 않고 Action을 통해서 간접적으로 트리거
//플레이어는 이동, idle 애니메이션을 트리거

public class Player : Unit
{
    #region Properties
    #region Private
    /// <summary>
    /// 마우스가 플레이어 기준 오른쪽에 있는지에 대한 bool값
    /// </summary>
    private bool isRight;
    #endregion
    #region Protected
    #endregion
    #region Public
    public MoveAction moveAction;
    public AttackAction attackAction;
    public PlayerAnimationController myAnim;
    public Transform graphicTransform;
    public GameObject weaponRotationAxis;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Player() : base()
    {
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void OnDead()
    {
        stateMachine.ChangeState<PlayerDeadState>();
    }
    protected override void Initialize()
    {
        base.Initialize();
        isRight = graphicTransform.localScale.x < 0;
        if(myAnim == null)
        {
            myAnim = GetComponentInChildren<PlayerAnimationController>();
        }
        InputController.Instance.getMousePosEvent.AddListener(OnGetMousePos);
        stateMachine.ChangeState<PlayerInitState>();
    }
    #endregion
    #region Public
    public GameObject GetWeaponRotationAxis()
    {
        return weaponRotationAxis;
    }
    #endregion
    #endregion

    #region EventHandlers
    /// <summary>
    /// InputController에서 위치를 받는 이벤트 함수
    /// 여기서 플레이어의 스프라이트를 뒤집는 역할을 함
    /// </summary>
    /// <param name="mousePos">InputController 클래스가 보내준 마우스 월드 위치</param>
    public void OnGetMousePos(Vector2 mousePos)
    {
        float Dot = Vector2.Dot(transform.right, mousePos - (Vector2)transform.position);
        
        //isRight와 XOR 연산해서 True가 나오면 이전값과 다르므로 위치가 달라졌다고 볼수 있다.
        //그때 Flip을 하는데 유니티 스프라이트 시스템이 아니므로 Scale의 x 값을 조정해서 한다.
        //isRight : ture, Dot >= 0 : true -> 오른쪽 바라보고 있고, 계산값 오른쪽 -> XOR값 false
        //ture, false -> 오른쪽 바라보고 있었고, 계산값 왼쪽 -> XOR 값 true
        //false, ture -> 왼쪽 바라보고 있었고, 계산값 오른쪽 -> XOR 값 true
        //false, false -> 왼쪽 바라보고 있었고, 계산값 왼쪽 -> XOR 값 false
        if(Dot >= 0 ^ isRight)
        {
            graphicTransform.localScale = new Vector3(-graphicTransform.localScale.x, graphicTransform.localScale.y, graphicTransform.localScale.z);
            isRight = !isRight;
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
