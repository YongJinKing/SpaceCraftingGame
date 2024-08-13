using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Structure
{
    public int consumeIndex1; // �ǹ� ���ۿ� �ʿ��� ���1
    public int consumeIndex2; // �ǹ� ���ۿ� �ʿ��� ���2
    public int consumeCount1; // ���1�� ����
    public int consumeCount2; // ���2�� ����
    public int produceCount; // ���귮
    public int maxAmount; // ���� ������ �ִ� �뷮
    public int produceResourceIndex; // �����ϴ� �ڿ��� �ε���
    [SerializeField] int produceAmount; // �ǹ��� ������ ����� ����
    [SerializeField] float produceTime; // �ǹ��� ���� �ӵ�

    public Transform miningVFX;

    public FactoryBuilding() : base()
    {
        AddStat(EStat.Efficiency, produceTime);

    }
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // �����Ұ� �����ϰ�
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

    IEnumerator FactoryWorking() // 1�ʿ� json���� ���ǵ� �縸ŭ(produceCount)��ŭ produceIndex�� �ش��ϴ� �������� �����Ѵ�
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

    public void FactoryClickEvent() // ���� �÷��̾ ��� ��Ʈ�� ���忡�� ������ ��Ḧ ���������� �� �� �Լ��� ȣ���� index�� �´� ������� ������ produceAmount��ŭ�� ���� ��Ḧ Add�Ѵ�
    {
        if(Inventory.instance != null) Inventory.instance.AddItem(produceResourceIndex, produceAmount);
        produceAmount = 0;
    }
}
