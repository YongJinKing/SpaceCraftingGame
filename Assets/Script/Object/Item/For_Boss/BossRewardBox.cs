using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRewardBox : MonoBehaviour
{
    [Header("보상을 스폰할 위치"), Space(.5f)]
    public Transform spawnPos;

    [Header("미네랄 자원 프리펩"), Tooltip("박스에서 드랍할 미네랄"), Space(.5f)]
    public GameObject mineralPrefab;

    [Header("가스 자원 프리펩"), Tooltip("박스에서 드랍할 가스"), Space(.5f)]
    public GameObject gasPrefab;

    [Header("보스 악세서리 프리펩"), Tooltip("박스에서 드랍할 보스 악세서리"), Space(.5f)]
    public GameObject specialAcce;

    [Header("다음 행성 좌표 아이템 프리펩"), Tooltip("박스에서 드랍할 다음 행성의 좌표를 알려줄 아이템"), Space(.5f)]
    public GameObject planetCoordinateItem;

    [Header("보상 생성시 나타날 VFX 프리펩"), Space(.5f)]
    public GameObject rewardsVFX;

    [SerializeField]
    [Header("드랍할 미네랄 갯수"), Tooltip("박스에서 드랍할 미네랄 갯수"), Space(.5f)]
    int mineralCnt;

    [SerializeField]
    [Header("드랍할 가스 갯수"), Tooltip("박스에서 드랍할 가스 갯수"), Space(.5f)]
    int gasCnt;

    private void OnEnable()
    {
        this.transform.position = spawnPos.position;
    }

    void SpawnRewards(GameObject spawnItem, int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            Instantiate(spawnItem, spawnPos.transform.position, Quaternion.identity, null);
        }
    }

    public void SpawnRewardsAct()
    {
        Instantiate(rewardsVFX, spawnPos.transform.position, Quaternion.identity, null);

        SpawnRewards(mineralPrefab, mineralCnt);
        SpawnRewards(gasPrefab, gasCnt);
        SpawnRewards(specialAcce, 1);
        SpawnRewards(planetCoordinateItem, 1);
    }
}
