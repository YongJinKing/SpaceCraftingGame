using System;

public struct WeaponDataStruct
{
    [Serializable]
    public struct StatModifyValue
    {
        public EStat stattype;              //���� Ÿ��
        public ValueModifierType modiType;  //���� ��� (0�̸� +, 1�̸� *)
        public int sortOrder;               //���� ���(0�� �������� ���� ����)
        public float value;
    }

    public int Index;
    public int Anim_Type;  //���߿� Enum���� �ٲ㵵 �ɰͰ���., -1�ϰ�� ���� ��������

    public StatModifyValue[] Stat_Modify_Values;    //�����ϴ� ���� ������ ���� ��İ� ��
        
    public int Main_Action_Index;   //ActionFactory ����
    public int Second_Action_Index; //ActionFactory ����
    public int Weapon_Prefab_Index; //�𵨰� bulletStartPos, �ϴ� ��� ������ BulletStartPos������ �ϴ°ɷ�
}
