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
    public int produceAmount; // 건물이 제작한 재료의 갯수
    [SerializeField] float produceTime; // 건물의 생산 속도

    public Transform miningVFX;
    public GameObject produceIcon;
    float mouseTime;

    [Header("매시 랜더러"), Space(.5f)]
    [SerializeField] MeshRenderer factoryMeshRender;
    [Header("오리지널 메테리얼"), Space(.5f)]
    [SerializeField] Material origin_Mat;
    [Header("아웃라인 메테리얼"), Space(.5f)]
    [SerializeField] Material outLine_Mat;
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
        produceIcon.SetActive(false);
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
    {/*
        yield return new WaitForSeconds(1f);
        */
        while (true)
        {
            yield return new WaitForSeconds(produceTime + ((1f - GetEfficiency()) * 2f));

            if (this[EStat.HP] <= 0.0f)
            {
                DestoryBuilding();
                break;
            }

            produceAmount += produceCount;
            if(!produceIcon.activeSelf) produceIcon.SetActive(true);

            if (produceAmount >= maxAmount)
            {
                produceAmount = maxAmount;
            }

            
            

            
        }
    }

    public void FactoryClickEvent() // 만약 플레이어가 어떠한 루트로 공장에서 생산한 재료를 가져가려할 때 이 함수를 호출해 index에 맞는 현재까지 생산한 produceAmount만큼의 양의 재료를 Add한다
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.AddItem(produceResourceIndex, produceAmount);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.itemSoundData.GetFactoryItem);
            produceAmount = 0;
            produceIcon.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseOver()
    {
        factoryMeshRender.material = outLine_Mat;
        mouseTime += Time.deltaTime;
        Debug.Log("Tq");
        if(mouseTime >= 1f)  buildingProduceAmountUI.ActiveAmountUI(this);
        
    }

    private void OnMouseExit()
    {
        factoryMeshRender.material = origin_Mat;
        mouseTime = 0;
        buildingProduceAmountUI.DeActiveAmountUI();
    }

}
