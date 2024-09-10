using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRewardBox : MonoBehaviour
{
    [Header("������ ������ ��ġ"), Space(.5f)]
    public Transform spawnPos;

    [Header("�̳׶� �ڿ� ������"), Tooltip("�ڽ����� ����� �̳׶�"), Space(.5f)]
    public GameObject mineralPrefab;

    [Header("���� �ڿ� ������"), Tooltip("�ڽ����� ����� ����"), Space(.5f)]
    public GameObject gasPrefab;

    [Header("���� �Ǽ����� ������"), Tooltip("�ڽ����� ����� ���� �Ǽ�����"), Space(.5f)]
    public GameObject specialAcce;

    [Header("���� �༺ ��ǥ ������ ������"), Tooltip("�ڽ����� ����� ���� �༺�� ��ǥ�� �˷��� ������"), Space(.5f)]
    public GameObject planetCoordinateItem;

    [Header("���� ������ ��Ÿ�� VFX ������"), Space(.5f)]
    public GameObject rewardsVFX;

    [SerializeField]
    [Header("����� �̳׶� ����"), Tooltip("�ڽ����� ����� �̳׶� ����"), Space(.5f)]
    int mineralCnt;

    [SerializeField]
    [Header("����� ���� ����"), Tooltip("�ڽ����� ����� ���� ����"), Space(.5f)]
    int gasCnt;

    private void OnEnable()
    {
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane);
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(viewportCenter);

        this.transform.position = new Vector3(worldCenter.x, worldCenter.y, 0);
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
