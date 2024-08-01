using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RiceRainPatternManager : MonoBehaviour
{
    public Tilemap tilemap; // 참조할 타일맵 
    public GameObject shapePrefab; // 떨어지는 위치를 알려주는 오브젝트 프리팹
    public GameObject waringPrefab; // 떨어지는 중인걸 알려주는 오브젝트 프리펩

    // 아래는 x,+ 모양의 떨어질 위치와 낙하 지점 표시 프리펩들의 부모들
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
        // 타일맵의 중심 위치 계산
        Vector3Int centerPos = new Vector3Int(tilemap.cellBounds.xMin + tilemap.cellBounds.size.x / 2,
                                                   tilemap.cellBounds.yMin + tilemap.cellBounds.size.y / 2,
                                                   0);

        // 십자(+) 모양과 X자 모양의 위치 계산 및 저장
        //StorePatternPositionsCrossNX(centerPos);

        // 체스판 모양으로 위치 계산 및 저장
        StorePatternPositionsChessboard();


        // 오브젝트 인스턴스화 및 비활성화
        InstantiateObjectsAtPositions(crossPatternPositions, crossShapes,crossPatternShapeParent, shapePrefab);
        InstantiateObjectsAtPositions(crossPatternPositions, crossWarnings,crossPatternWaringParent, waringPrefab);
        InstantiateObjectsAtPositions(xPatternPositions, xShapes, xPatternShapeParent, shapePrefab);
        InstantiateObjectsAtPositions(xPatternPositions, xWarnings, xPatternWaringParent, waringPrefab);

        RiceRainAttack.SetLists(xShapes, xWarnings, crossShapes, crossWarnings);
    }

    void StorePatternPositionsCrossNX(Vector3Int center)
    {
        // 십자(+) 모양 위치 저장
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

        // X자 모양 위치 저장
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

        // 체스판처럼 한칸 간격을 두고 나누기
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
