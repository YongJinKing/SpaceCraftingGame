using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Structure
{
    // �ǹ� ���ۿ� �ʿ��� ���, ��� ����, ���� ���, ���� ����
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
        // �����Ұ� �����ϰ�
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
            StartProducing(); // �ӽ�, �۵� Ȯ�ο�
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

    IEnumerator FactoryWorking() // 1�ʿ� json���� ���ǵ� �縸ŭ(produceCount)��ŭ produceIndex�� �ش��ϴ� �������� �����Ѵ�
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

    public void FactoryClickEvent() // ���� �÷��̾ ��� ��Ʈ�� ���忡�� ������ ��Ḧ ���������� �� �� �Լ��� ȣ���� index�� �´� ������� ������ produceAmount��ŭ�� ���� ��Ḧ Add�Ѵ�
    {
        Inventory.instance.AddItem(produceIndex, produceAmount);
    }
}
