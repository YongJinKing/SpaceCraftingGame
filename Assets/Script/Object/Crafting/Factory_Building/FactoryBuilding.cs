using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Structure
{
    // 건물 제작에 필요한 재료, 재료 갯수, 생산 목록, 생산 갯수
    public int consumeIndex;
    public int consumeCount;
    public int produceIndex;
    public int produceCount;
    public int maxAmount;
    [SerializeField] int produceAmount;
    [SerializeField] float produceTime;

    public FactoryBuilding() : base()
    {
        AddStat(EStat.Efficiency, produceTime);

    }
    public override void TakeDamage(float damage)
    {
        float dmg = damage - this[EStat.DEF];
        if (dmg <= 0.0f) dmg = 1f;
        this[EStat.HP] = dmg;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 설정할거 설정하고
        produceTime = this[EStat.Efficiency];

    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartProducing(); // 임시, 작동 확인용
        }
    }

    public void StartProducing()
    {
        StartCoroutine(FactoryWorking());
    }
    public void DestoryBuilding()
    {
        StopCoroutine(FactoryWorking());
        Destroy(this.gameObject);
    }

    IEnumerator FactoryWorking() // 1초에 json으로 정의된 양만큼(produceCount)만큼 produceIndex에 해당하는 아이템을 생산한다
    {
        while (true)
        {
            if (this[EStat.HP] <= 0.0f)
            {
                DestoryBuilding();
                break;
            }

            produceAmount += produceCount;
            

            if (produceAmount >= maxAmount)
            {
                produceAmount = maxAmount;
            }

            
            yield return new WaitForSeconds(produceTime);

            
        }
    }

    public void FactoryClickEvent() // 만약 플레이어가 어떠한 루트로 공장에서 생산한 재료를 가져가려할 때 이 함수를 호출해 index에 맞는 현재까지 생산한 produceAmount만큼의 양의 재료를 Add한다
    {
        Inventory.instance.AddItem(produceIndex, produceAmount);
    }
}
