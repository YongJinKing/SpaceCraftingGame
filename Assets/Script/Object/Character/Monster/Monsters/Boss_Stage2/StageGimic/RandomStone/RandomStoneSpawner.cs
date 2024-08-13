using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTile
{
    public bool placeable;

    public BossTile(bool placeable)
    {
        this.placeable = placeable;
    }
}

public class RandomStoneSpawner : MonoBehaviour
{
    public GimicStone stone;
    public Tilemap bossTilemap;
    public Dictionary<Vector3Int, BossTile> placeablePlaces;
    public LayerMask layerMask;
    public Transform warningSign;

    Coroutine spawner;
    // Start is called before the first frame update
    void Start()
    {
        placeablePlaces = new Dictionary<Vector3Int, BossTile>();
        for (int y = bossTilemap.cellBounds.yMin; y < bossTilemap.cellBounds.yMax; y++)
        {
            for (int x = bossTilemap.cellBounds.xMin; x < bossTilemap.cellBounds.xMax; x++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)bossTilemap.transform.position.z));
                Vector3 place = bossTilemap.CellToWorld(localPlace);
                if (bossTilemap.HasTile(localPlace))
                {
                    Vector3Int _place = new Vector3Int((int)place.x, (int)place.y, (int)place.z);
                    //Tile at "place"
                    placeablePlaces[_place] = new BossTile(true);
                }
                else
                {
                    //No tile at "place"
                }
            }
        } // 보스 스테이지의 타일맵을 모두 '배치 가능'으로 초기화

        StartSpawnStones();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if(spawner != null) StopCoroutine(spawner);
        }
    }
    bool CheckObjectsInArea(Vector3Int startPosition, float size, LayerMask layerMask)
    {
        // 박스 중심 위치 계산
        Vector3 centerPosition = startPosition + new Vector3(size / 2, size / 2, 0);

        // NxN 크기의 박스 영역 내에 있는 오브젝트를 검사
        Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(size, size), 0f, layerMask);

        // 오브젝트가 있는지 확인
        return colliders.Length > 0;
    }
    
    IEnumerator SpawnWarningSign(Vector3 pos)
    {
        Instantiate(warningSign, pos, Quaternion.identity, null);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SpawnStoneAtRandomPlace()
    {
        while (true)
        {
            float randomTimer = Random.Range(1f, 3f);
            int x = Random.Range(bossTilemap.cellBounds.xMin + 1, bossTilemap.cellBounds.xMax - 1);
            int y = Random.Range(bossTilemap.cellBounds.yMin + 1, bossTilemap.cellBounds.yMax - 1);
            Vector3Int localPlace = (new Vector3Int(x, y, (int)bossTilemap.transform.position.z));

            if (!bossTilemap.HasTile(localPlace))
            {
                continue;
            }

            Vector3 place = bossTilemap.CellToWorld(localPlace);
            Vector3Int _place = new Vector3Int((int)place.x, (int)place.y, (int)place.z);

            if (!placeablePlaces[_place].placeable)
            {
                continue;
            }

            if (CheckObjectsInArea(_place, 5, layerMask))
            {
                continue;
            }

            placeablePlaces[_place].placeable = false;
            
            yield return StartCoroutine(SpawnWarningSign(place));

            Vector3 spawnPos = place + new Vector3(0f, Camera.main.orthographicSize + 15, 0f); // 메인 카메라의 사이즈보다 10칸 위로 보내서 거기서 떨어트린다.
            var obj = Instantiate(stone, spawnPos, Quaternion.identity, null);

            // Rigidbody2D 컴포넌트를 추가하여 중력을 적용
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody2D>();
            }

            // 중력을 설정 (기본 중력 가속도를 사용)
            rb.gravityScale = 2f;

            obj.GetComponent<GimicStone>().Initialize(place);

            yield return new WaitForSeconds(randomTimer);

        }
    }



    void StartSpawnStones()
    {
        spawner = StartCoroutine(SpawnStoneAtRandomPlace());
    }
    
}
