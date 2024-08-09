using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;
using static UnityEngine.GraphicsBuffer;

public class BossAlertState : BossState
{
    #region Properties
    #region Private
    float limitDist = 5f;
    float moveSpeed = 1f;
    int idx = 0;
    Queue<int> shuffledNormalIndices;
    Queue<int> shuffledSpecialIndices;
    bool specialToggle = false;
    Transform bossImg;
    #endregion
    #region Protected
    
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
    protected override void Awake()
    {
        base.Awake();
        InitializeShuffledNormalIndices();
        InitializeShuffledSpecialIndices();
    }
    private void Start()
    {
        
    }

    // attackActions의 인덱스 번호들을 랜덤으로 섞고 큐에 넣은 뒤 하나하나 차례대로 빼내어 쓰는 형식
    // 큐가 비었다면 다시 인덱스 번호들을 랜덤으로 섞어 큐에 넣는다.
    void InitializeShuffledNormalIndices()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < owner.attackActions.Length; i++)
        {
            indices.Add(i);
        }
        Shuffle(indices);

        shuffledNormalIndices = new Queue<int>(indices);
    }

    void InitializeShuffledSpecialIndices()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < owner.specialActions.Length; i++)
        {
            indices.Add(i);
        }
        Shuffle(indices);

        shuffledSpecialIndices = new Queue<int>(indices);
    }

    void Shuffle(List<int> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = random.Next(i, n);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    int GetNextRandomIndex()
    {
        if (shuffledNormalIndices.Count == 0)
        {
            InitializeShuffledNormalIndices();
        }
        return shuffledNormalIndices.Dequeue();
    }

    int GetNextRandomSpecialIndex()
    {
        if (shuffledSpecialIndices.Count == 0)
        {
            InitializeShuffledSpecialIndices();
        }
        return shuffledSpecialIndices.Dequeue();
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        BossMovement bossMovement = GetComponent<BossMovement>();
        if (bossMovement != null)
        {
            limitDist = bossMovement.limitDist;
        }
        
        //StartCoroutine(ProcessingState()); // 이동이 끝난 뒤에 State변경을 해야함..
        StartCoroutine(FollowAndRotate());
    }
    public override void Exit()
    {
        base.Exit();
        
        StopAllCoroutines();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator FollowAndRotate()
    {
        owner.animator.SetBool("Move", true);
        while (true)
        {
            // 1. 플레이어에게 일정 거리까지 다가간다.
            yield return StartCoroutine(MoveTowardsTarget());

        }
    }

    private IEnumerator MoveTowardsTarget()
    {
        bossImg = this.transform.GetChild(this.transform.childCount - 1);
        while (true)
        {
            if (owner.target.transform.position.x > this.transform.position.x)
            {
                bossImg.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (owner.target.transform.position.x < this.transform.position.x)
            {
                bossImg.transform.localScale = new Vector3(1, 1, 1);
            }

            Vector2 dir = (Vector2)owner.target.transform.position - (Vector2)transform.position;
            float dist = dir.magnitude - 0.5f;
            if (dist <= limitDist)
            {
                break;
            }

            dir.Normalize();
            transform.Translate(dir * owner[EStat.MoveSpeed] * Time.deltaTime, Space.World);
            yield return null;
        }

        yield return StartCoroutine(ProcessingState());
    }
    private IEnumerator RotateAroundTarget()
    {
        float startTime = Time.time;
        float rotateTime = Random.Range(1f, 4f);
        Vector2 initialPosition = transform.position;
        Vector2 targetPosition = owner.target.transform.position; // 회전 시작 시의 플레이어 위치 고정
        float angle = 0f;

        Vector2 relativePosition = initialPosition - targetPosition;
        float initialAngle = Mathf.Atan2(relativePosition.y, relativePosition.x);

        while (Time.time - startTime < rotateTime)
        {
            angle = initialAngle + 0.5f * (Time.time - startTime);

            // 새로운 위치 계산
            float x = Mathf.Cos(angle) * limitDist;
            float y = Mathf.Sin(angle) * limitDist;

            transform.position = new Vector3(targetPosition.x + x, targetPosition.y + y, transform.position.z);
            yield return null;
        }
    }

    protected IEnumerator ProcessingState()
    {
        //Wait until Select Action
        owner.animator.SetBool("Move", false);
        if (owner.activatedAction != null) yield return new WaitForSeconds(owner.activatedAction.delayTime);
        yield return StartCoroutine(SelectingAction());
        
        owner.stateMachine.ChangeState<BossAttackState>();
        
    }

    protected IEnumerator SelectingAction()
    {
        if (owner.attackActions == null)
        {
            yield break;
        }

        Action action = null;
        
        if (!specialToggle)
        {
            idx = GetNextRandomIndex();
            specialToggle = !specialToggle;
            action = owner.attackActions[idx];
        }
        else
        {
            idx = GetNextRandomSpecialIndex();
            specialToggle = !specialToggle;
            action = owner.specialActions[idx];
        }

        owner.activatedAction = action;
    }

    #endregion

    #region MonoBehaviour
    #endregion
}
