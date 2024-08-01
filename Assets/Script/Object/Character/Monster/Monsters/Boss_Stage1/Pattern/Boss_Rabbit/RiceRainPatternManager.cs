using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RiceRainPatternManager : MonoBehaviour
{
    public Tilemap tilemap; // ������ Ÿ�ϸ� 
    public GameObject shapePrefab; // �������� ��ġ�� �˷��ִ� ������Ʈ ������
    public GameObject waringPrefab; // �������� ���ΰ� �˷��ִ� ������Ʈ ������

    // �Ʒ��� x,+ ����� ������ ��ġ�� ���� ���� ǥ�� ��������� �θ��
    public Transform xPatternShapeParent;
    public Transform xPatternWaringParent;
    public Transform crossPatternShapeParent;
    public Transform crossPatternWaringParent;

    public int interval;

    public SP_RiceRainAttack RiceRainAttack;

    List<Transform> xShapes = new List<Transform>();
    List<Transform> xWarnings = new List<Transform>();
    List<Transform> crossShapes = new List<Transform>();
    List<Transform> crossWarnings = new List<Transform>();

    List<Vector3Int> crossPatternPositions = new List<Vector3Int>();
    List<Vector3Int> xPatternPositions = new List<Vector3Int>();

    void Start()
    {
        // Ÿ�ϸ��� �߽� ��ġ ���
        Vector3Int centerPos = new Vector3Int(tilemap.cellBounds.xMin + tilemap.cellBounds.size.x / 2,
                                                   tilemap.cellBounds.yMin + tilemap.cellBounds.size.y / 2,
                                                   0);

        // ����(+) ���� X�� ����� ��ġ ��� �� ����
        //StorePatternPositionsCrossNX(centerPos);

        // ü���� ������� ��ġ ��� �� ����
        StorePatternPositionsChessboard();


        // ������Ʈ �ν��Ͻ�ȭ �� ��Ȱ��ȭ
        InstantiateObjectsAtPositions(crossPatternPositions, crossShapes,crossPatternShapeParent, shapePrefab);
        InstantiateObjectsAtPositions(crossPatternPositions, crossWarnings,crossPatternWaringParent, waringPrefab);
        InstantiateObjectsAtPositions(xPatternPositions, xShapes, xPatternShapeParent, shapePrefab);
        InstantiateObjectsAtPositions(xPatternPositions, xWarnings, xPatternWaringParent, waringPrefab);

        RiceRainAttack.SetLists(xShapes, xWarnings, crossShapes, crossWarnings);
    }

    void StorePatternPositionsCrossNX(Vector3Int center)
    {
        // ����(+) ��� ��ġ ����
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                crossPatternPositions.Add(new Vector3Int(x, center.y + offset, 0));
            }
        }
        for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                crossPatternPositions.Add(new Vector3Int(center.x + offset, y, 0));
            }
        }

        // X�� ��� ��ġ ����
        int minDimension = Mathf.Min(tilemap.cellBounds.size.x, tilemap.cellBounds.size.y);
        for (int i = -minDimension; i <= minDimension; i++)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                Vector3Int diagonal1 = new Vector3Int(center.x + i, center.y + i + offset, 0);
                Vector3Int diagonal2 = new Vector3Int(center.x + i, center.y - i + offset, 0);
                if (tilemap.cellBounds.Contains(diagonal1))
                {
                    xPatternPositions.Add(diagonal1);
                }
                if (tilemap.cellBounds.Contains(diagonal2))
                {
                    xPatternPositions.Add(diagonal2);
                }
            }
        }
    }

    void StorePatternPositionsChessboard()
    {
        BoundsInt bounds = tilemap.cellBounds;

        // ü����ó�� ��ĭ ������ �ΰ� ������
        /*for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if ((x + y) % 2 == 0)
                {
                    crossPatternPositions.Add(position);
                }
                else
                {
                    xPatternPositions.Add(position);
                }
            }
        }*/

        for (int x = bounds.xMin; x < bounds.xMax; x += interval)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y += interval)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if ((x / interval + y / interval) % 2 == 0)
                {
                    crossPatternPositions.Add(position);
                }
                else
                {
                    xPatternPositions.Add(position);
                }
            }
        }
    }

    void InstantiateObjectsAtPositions(List<Vector3Int> positions, List<Transform> list,Transform parent, GameObject objectPrefab)
    {
        foreach (Vector3Int position in positions)
        {
            Vector3 worldPosition = tilemap.CellToWorld(position);
            GameObject obj = Instantiate(objectPrefab, worldPosition, Quaternion.identity, parent);
            list.Add(obj.transform);
            obj.SetActive(false);
        }
    }
}
