using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Structure
{
    public int consumeIndex1; // 건물 제작에 필요한 재료1
    public int consumeIndex2; // 건물 제작에 필요한 재료2
    public int consumeCount1; // 재료1의 갯수
    public int consumeCount2; // 재료2의 갯수
    public int produceCount; // 생산량
    public int maxAmount; // 저장 가능한 최대 용량
    public int produceResourceIndex; // 제작하는 자원의 인덱스
    [SerializeField] int produceAmount; // 건물이 제작한 재료의 갯수
    [SerializeField] float produceTime; // 건물의 생산 속도

    public Transform miningVFX;

    public FactoryBuilding() : base()
    {
        AddStat(EStat.Efficiency, produceTime);

    }
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 설정할거 설정하고
        produceTime = this[EStat.Efficiency];
        this.DestroyEvent.AddListener(TileManager.Instance.DestoryObjectOnTile);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartProducing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartProducing()
    {
        StartCoroutine(FactoryWorking());
    }
    public void DestoryBuilding()
    {
        StopCoroutine(FactoryWorking());
    }

    protected override void OnDead()
    {
        base.OnDead();
        DestoryBuilding();
    }

    public void TurnOnMiningVFX()
    {
        if(!miningVFX.gameObject.activeSelf) miningVFX.gameObject.SetActive(true);
    }

    public void TurnOffMiningVFX()
    {
        if (!miningVFX.gameObject.activeSelf) miningVFX.gameObject.SetActive(false);
    }

    IEnumerator FactoryWorking() // 1초에 json으로 정의된 양만큼(produceCount)만큼 produceIndex에 해당하는 아이템을 생산한다
    {
        yield return new WaitForSeconds(1f);
        
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

            
            yield return new WaitForSeconds(produceTime + ((1f - GetEfficiency()) * 2f));

            
        }
    }

    public void FactoryClickEvent() // 만약 플레이어가 어떠한 루트로 공장에서 생산한 재료를 가져가려할 때 이 함수를 호출해 index에 맞는 현재까지 생산한 produceAmount만큼의 양의 재료를 Add한다
    {
        if(Inventory.instance != null) Inventory.instance.AddItem(produceResourceIndex, produceAmount);
        produceAmount = 0;
    }
}
