using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rabbit : Monster
{

    public Boss_Rabbit() : base()
    {
        AddStat(EStat.DetectRadius, 0.0f);
    }
    protected override void Initialize()
    {
        base.Initialize();
        spawnPoint = transform.position;

        stateMachine.ChangeState<RabbitBossInitState>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
