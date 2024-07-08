using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAction : AttackAction
{
    public Unit owner; // <<< �����̴� ������ ���ε�, ���� ���� �׼��̴ϱ� ������ ������ ���ε��Ѵ�.
    
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
