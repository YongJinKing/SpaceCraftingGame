using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SpaceShip : Structure
{
    public List<BuildDron> builderDrons;
    public Tilemap tileMap;
    public Transform gameOverCanvas;
    public Transform spaceShipImg;
    
    public bool IsDronReady()
    {
        return builderDrons.Count > 0;
    }

    public void TakeOffDron(Transform target)
    {
        if (builderDrons.Count == 0)
        {
            return;
        }

        builderDrons[0].SetTarget(target);
        builderDrons[0].dronImg.gameObject.SetActive(true);
        builderDrons[0].StartWorking();
        builderDrons.RemoveAt(0);
    }

    public void PutInDron(BuildDron dron)
    {
        builderDrons.Add(dron);
        dron.dronImg.gameObject.SetActive(false);
    }


    protected override void Initialize()
    {
        base.Initialize();
        // 스텟은 여기서 결정하면 됨, 위치도 결정해버리자
        int centerY = (tileMap.cellBounds.yMin + tileMap.cellBounds.yMax) / 2;
        int centerX = (tileMap.cellBounds.xMin + tileMap.cellBounds.xMax) / 2;
        Vector3 pos = new Vector3(centerX, centerY, 0);

        this.transform.position = pos;
    }

    protected override void OnDead()
    {
        deadEvent?.Invoke();
        //animator.SetTrigger("Destroy");
        spaceShipImg.gameObject.SetActive(false);
        this.GetComponent<BoxCollider2D>().enabled = false;
        DestroyEvent?.Invoke(this.transform.position);
        StartCoroutine(SpawnGameOver());
    }

    IEnumerator SpawnGameOver()
    {
        Instantiate(destroyVFX, this.transform.position, Quaternion.identity, null);
        yield return new WaitForSeconds(2f);
        Instantiate(gameOverCanvas, null);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            TakeDamage(10000f);
        }
    }

}
