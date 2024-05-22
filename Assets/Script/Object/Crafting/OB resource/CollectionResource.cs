using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionResource : MonoBehaviour
{
    private MineralSpawner spawner;

    public void Initialize(MineralSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnMouseDown()
    {
        // 미네랄이 채취되었을 때 새로운 미네랄을 생성
        spawner.PlaceMinerals();

        // 미네랄 오브젝트 삭제
        Destroy(gameObject);
    }
}
