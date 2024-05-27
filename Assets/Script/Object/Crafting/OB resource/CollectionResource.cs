using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectionResource : MonoBehaviour
{
    public delegate void MineralHarvested(Vector3Int position);
    public static event MineralHarvested OnMineralHarvested;

    private Tilemap tilemap;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    // 미네랄 채취 메서드
    public void Harvest()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        OnMineralHarvested?.Invoke(cellPosition);
        Destroy(gameObject); // 미네랄 오브젝트 제거
    }


}
