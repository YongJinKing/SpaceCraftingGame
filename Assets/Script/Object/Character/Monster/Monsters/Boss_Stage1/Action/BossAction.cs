using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAction : AttackAction
{
    public Unit owner; // <<< 움직이는 유닛을 바인딩, 여긴 보스 액션이니깐 보스를 가져다 바인딩한다.

    [SerializeField] protected CamController camController;
    protected Animator ownerAnim;
    public override void Deactivate()
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ownerAnim = owner.animator;
        camController = FindFirstObjectByType<CamController>();
    }

    protected void SetRabbitLookPlayer(Vector2 target)
    {
        int childCount = owner.transform.childCount;
        Transform rabbit = owner.transform.GetChild(childCount - 1);

        if (rabbit.transform.position.x > target.x)
        {
            rabbit.transform.localScale = new Vector3(1, 1, 1); // 왼쪽 바라보기
        }
        else
        {
            rabbit.transform.localScale = new Vector3(-1, 1, 1); // 오른쪽 바라보기
        }
    }

    protected void SetRabbitLookPlayer(Transform target)
    {
        int childCount = owner.transform.childCount;
        Transform rabbit = owner.transform.GetChild(childCount - 1);

        if (rabbit.transform.position.x > target.transform.position.x)
        {
            rabbit.transform.localScale = new Vector3(1, 1, 1); // 왼쪽 바라보기
        }
        else
        {
            rabbit.transform.localScale = new Vector3(-1, 1, 1); // 오른쪽 바라보기
        }
    }
}
