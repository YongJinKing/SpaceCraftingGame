using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpaceShipInfo
{
    public float Hp;

    public SpaceShipInfo(float Hp)
    {
        this.Hp = Hp;
    }

}

public class SpaceShipHPSaveManager : BaseSaveSystem
{
    public SpaceShip spaceship;
    public string savePath;
    float hp;
    bool inited = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        savePath = Path.Combine(filePath, "SpaceShipHP_" + DataManager.Instance.nowSlot + ".json");
        inited = true;
    }

    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        hp = spaceship[EStat.HP];

        SpaceShipInfo sSInfo = new SpaceShipInfo(hp);

        var json = JsonConvert.SerializeObject(sSInfo, Formatting.Indented);

        File.WriteAllText(savePath, json);

        if (File.Exists(savePath))
        {
            Debug.Log("������ ���������� ����Ǿ����ϴ�: " + savePath);
        }
        else
        {
            Debug.LogError("���� ���忡 �����߽��ϴ�: " + savePath);
        }
    }

    public void LoadHP()
    {
        if (!inited) Start();

        float savedHP = 0f;
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SpaceShipInfo data = JsonUtility.FromJson<SpaceShipInfo>(json);
            savedHP = data.Hp;
            Debug.Log("����� ������ ã�ҽ��ϴ�: " + savePath + ", " + savedHP);

            spaceship.SetSpaceshipLoadedHP(savedHP);

        }
        else
        {
            Debug.Log(savePath + " ><><><><><><><><>");
            Debug.Log("���ּ� ü�� ���� ����");
        }
    }
}
