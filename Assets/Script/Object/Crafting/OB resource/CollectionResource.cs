using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectionResource : MonoBehaviour
{
    public delegate void MineralHarvested(Vector3Int position);
    public static event MineralHarvested OnMineralHarvested;

    public delegate void GasHarvested(Vector3Int position);
    public static event GasHarvested OnGasHarvested;

    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>();
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    // �̳׶� ä�� �޼���
    public void HarvestMineral()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        OnMineralHarvested?.Invoke(cellPosition);
        Destroy(gameObject); // �̳׶� ������Ʈ ����
    }

    // ���� ä�� �޼���
    public void HarvestGas()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        OnGasHarvested?.Invoke(cellPosition);
        Destroy(gameObject); // ���� ������Ʈ ����
    }

    public void OccupyTile(Vector3Int tilePosition)
    {
        occupiedTiles.Add(tilePosition);
    }

    public void FreeTile(Vector3Int tilePosition)
    {
        occupiedTiles.Remove(tilePosition);
    }

    public bool IsTileOccupied(Vector3Int tilePosition)
    {
        return occupiedTiles.Contains(tilePosition);
    }
}

