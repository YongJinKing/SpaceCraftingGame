using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAction : AttackAction
{
    public Unit owner; // <<< 움직이는 유닛을 바인딩, 여긴 보스 액션이니깐 보스를 가져다 바인딩한다.
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
