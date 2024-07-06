using UnityEngine;

//�÷��̾�� ���� �ִϸ��̼��� Ʈ�������� �ʰ� Action�� ���ؼ� ���������� Ʈ����
//�÷��̾�� �̵�, idle �ִϸ��̼��� Ʈ����

public class Player : Unit
{
    #region Properties
    #region Private
    /// <summary>
    /// ���콺�� �÷��̾� ���� �����ʿ� �ִ����� ���� bool��
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
    /// InputController���� ��ġ�� �޴� �̺�Ʈ �Լ�
    /// ���⼭ �÷��̾��� ��������Ʈ�� ������ ������ ��
    /// </summary>
    /// <param name="mousePos">InputController Ŭ������ ������ ���콺 ���� ��ġ</param>
    public void OnGetMousePos(Vector2 mousePos)
    {
        float Dot = Vector2.Dot(transform.right, mousePos - (Vector2)transform.position);
        
        //isRight�� XOR �����ؼ� True�� ������ �������� �ٸ��Ƿ� ��ġ�� �޶����ٰ� ���� �ִ�.
        //�׶� Flip�� �ϴµ� ����Ƽ ��������Ʈ �ý����� �ƴϹǷ� Scale�� x ���� �����ؼ� �Ѵ�.
        //isRight : ture, Dot >= 0 : true -> ������ �ٶ󺸰� �ְ�, ��갪 ������ -> XOR�� false
        //ture, false -> ������ �ٶ󺸰� �־���, ��갪 ���� -> XOR �� true
        //false, ture -> ���� �ٶ󺸰� �־���, ��갪 ������ -> XOR �� true
        //false, false -> ���� �ٶ󺸰� �־���, ��갪 ���� -> XOR �� false
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
