using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;
using System.Linq;

[Serializable]
public class MonsterDataManager : Singleton<MonsterDataManager>
{
    string savePath;
    public LayerMask EnemyLayerMask;

    public MonsterDataStrurct[] md = new MonsterDataStrurct[1];

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveMonsterInfo();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadJson();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i= 0; i < md.Length; ++i)
            {
                Debug.Log($"{md[i].Index}, {md[i].MaxHP}, {md[i].Priority}, {md[i].moveSpeed}, {md[i].ATK} {md[i].ATKSpeed}");
            }    
        }
    }

    public void SaveMonsterInfo()
    {
        //1. 유닛 찾기
        //2. 유닛이 플레이어 레이어 인지 확인
        //3. 플레이어의 데이터를 가져와 pd[0]에 저장
        //4. 직렬화
        //5. 파일로 저장
        //6. 디버그 로그

        //1.
        Unit[] units = FindObjectsOfType<Unit>();

        foreach(var monster in units)
        {
            if(monster.gameObject != null)
            {
                //2.
                if((EnemyLayerMask & 1 << monster.gameObject.layer)!=0)
                {
                    //3.
                    Monster temMo = monster.GetComponent<Monster>();

                    md[0].MaxHP = temMo.GetRawStat(EStat.MaxHP);
                    md[0].moveSpeed = temMo.GetRawStat(EStat.MoveSpeed);
                    md[0].ATK = temMo.GetRawStat(EStat.ATK);
                    md[0].ATKSpeed = temMo.GetRawStat(EStat.ATKSpeed);
                    md[0].Priority = temMo.GetRawStat(EStat.Priority);
                }
            }
        }

        //4.
        //직렬화 코드
        var json = JsonConvert.SerializeObject(md, Formatting.Indented);

        //5.
        savePath = "MonsterDataStrurct.json";
        File.WriteAllText(savePath, json);

        //6.
        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }

    private void LoadJson()
    {
        Debug.Log("LoadJson called");

        var jsonPath = "MonsterDataStrurct.json";
        if(File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            md = JsonConvert.DeserializeObject<MonsterDataStrurct[]>(JsonString);

            Dictionary<int, MonsterDataStrurct> monsterdataDic = new Dictionary<int, MonsterDataStrurct>();
            monsterdataDic = md.ToDictionary(x => x.Index);
        }
        else
        {
            Debug.Log("없음");
        }
    }
}
