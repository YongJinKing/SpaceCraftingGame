using UnityEngine;
using UnityEngine.Events;

//�÷��̾�� ���� �ִϸ��̼��� Ʈ�������� �ʰ� Action�� ���ؼ� ���������� Ʈ����
//�÷��̾�� �̵�, idle �ִϸ��̼��� Ʈ����

public class Player : Unit
{
    #region Properties
    #region Private
    public string Name;
    /// <summary>
    /// ���콺�� �÷��̾� ���� �����ʿ� �ִ����� ���� bool��
    /// </summary>
    private bool isRight;
    private bool _isDead = false;
    #endregion
    #region Protected
    #endregion
    #region Public
    /// <summary>
    /// mainAction�� secondAction�� �ӽ�
    /// ���߿� �����ۿ��� �׼��� �����ðǵ� �迭�� ����°� ������
    /// #need to modify later
    /// </summary>
    public Action mainAction;
    public Action secondAction;
    public PlayerAnimationController myAnim;
    public Transform graphicTransform;
    public WeaponRotationAxis weaponRotationAxis;
    /// <summary>
    /// 0 = ����Idle, 1 = �Ǽ� Idle
    /// </summary>
    public int modeType = 0;
    public bool isDead
    {
        get { return _isDead; }
    }
    public bool canEquip = false;
    internal int layer;
    #endregion
    #region Events
    public UnityEvent<int> UIChangeEvent = new UnityEvent<int>();
    public UnityEvent<bool> PlayerModeChangeEvent = new UnityEvent<bool>();
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
        base.OnDead();
        if (!isDead)
        {
            stateMachine.ChangeState<PlayerDeadState>();
            _isDead = true;
            InputController.Instance.getMousePosEvent.RemoveListener(OnGetMousePos);
        }
    }
    protected void OnHit()
    {
        stateMachine.ChangeState<PlayerHitState>();
    }
    protected override void Initialize()
    {
        base.Initialize();
        isRight = graphicTransform.localScale.x < 0;
        if (myAnim == null)
        {
            myAnim = GetComponentInChildren<PlayerAnimationController>();
        }
        InputController.Instance.getMousePosEvent.AddListener(OnGetMousePos);
        TimeManager time = FindObjectOfType<TimeManager>();
        if (time != null) 
        {
            time.dayChangeHealing.AddListener(OnDayChangeHealing);
        }
        if(FindObjectOfType<SpaceShipEnter>() != null) FindObjectOfType<SpaceShipEnter>().UIEnterEvent.AddListener(OnEnterUIState);
        stateMachine.ChangeState<PlayerInitState>();

        PlayerDataStruct loadedPD = DataManager.Instance.LoadJson(DataManager.Instance.tempSavePath);

        if (loadedPD.Index == -1) return;

        this[EStat.MaxHP] = loadedPD.MaxHP;
        this[EStat.HP] = loadedPD.HP;
        this[EStat.MoveSpeed] = loadedPD.MoveSpeed;
        this[EStat.ATK] = loadedPD.ATK;
        this[EStat.ATKSpeed] = loadedPD.ATKSpeed;
        this[EStat.Priority] = loadedPD.Priority;
    }
    #endregion
    #region Public
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (!isDead)
        {
            OnHit();
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    /// <summary>
    /// InputController���� ��ġ�� �޴� �̺�Ʈ �Լ�
    /// ���⼭ �÷��̾��� ��������Ʈ�� ������ ������ ��
    /// �ִϸ��̼� ��Ʈ�ѷ��� ����� �ű��?
    /// </summary>
    /// <param name="mousePos">InputController Ŭ������ ������ ���콺 ���� ��ġ</param>
    public void OnGetMousePos(Vector2 mousePos)
    {
        if (Time.timeScale < 0.01f)
            return;

        float Dot = Vector2.Dot(transform.right, mousePos - (Vector2)transform.position);

        //isRight�� XOR �����ؼ� True�� ������ �������� �ٸ��Ƿ� ��ġ�� �޶����ٰ� ���� �ִ�.
        //�׶� Flip�� �ϴµ� ����Ƽ ��������Ʈ �ý����� �ƴϹǷ� Scale�� x ���� �����ؼ� �Ѵ�.
        //isRight : ture, Dot >= 0 : true -> ������ �ٶ󺸰� �ְ�, ��갪 ������ -> XOR�� false
        //ture, false -> ������ �ٶ󺸰� �־���, ��갪 ���� -> XOR �� true
        //false, ture -> ���� �ٶ󺸰� �־���, ��갪 ������ -> XOR �� true
        //false, false -> ���� �ٶ󺸰� �־���, ��갪 ���� -> XOR �� false
        if (Dot >= 0 ^ isRight)
        {
            graphicTransform.localScale = new Vector3(-graphicTransform.localScale.x, graphicTransform.localScale.y, graphicTransform.localScale.z);
            isRight = !isRight;
        }
    }

    public void OnEnterUIState()
    {
        stateMachine.ChangeState<PlayerEnterUIState>();
    }

    public void OnDayChangeHealing()
    {
        if (this[EStat.HP] + 1.0f < this[EStat.MaxHP])
            this[EStat.HP] = this.GetRawStat(EStat.HP) + 1.0f;
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour

    protected override void Start()
    {
        base.Start();
    }
    #endregion
}
