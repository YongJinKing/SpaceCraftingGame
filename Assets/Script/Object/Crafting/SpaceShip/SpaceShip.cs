using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SpaceShip : Structure
{
    public List<BuildDron> builderDrons;
    public Tilemap tileMap;
    public Transform gameOverCanvas;
    public Transform spaceShipImg;
    public Transform spaceShipUnderAttackUI;
    public UnityEvent<int> dronCountChangeAct;
    public TimeManager timeManager;
    public SpaceShipHPSaveManager spaceShipHPSaveManager;
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
        dronCountChangeAct?.Invoke(builderDrons.Count);
    }

    public void PutInDron(BuildDron dron)
    {
        builderDrons.Add(dron);
        dron.dronImg.gameObject.SetActive(false);
        dronCountChangeAct?.Invoke(builderDrons.Count);
    }


    protected override void Initialize()
    {
        base.Initialize();
        int centerY = (tileMap.cellBounds.yMin + tileMap.cellBounds.yMax) / 2;
        int centerX = (tileMap.cellBounds.xMin + tileMap.cellBounds.xMax) / 2;
        Vector3 pos = new Vector3(centerX, centerY, 0);

        this.transform.position = pos;

    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.spaceShipAttacked, false);
        spaceShipUnderAttackUI.gameObject.SetActive(true);
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

    public void HealAday()
    {
        this[EStat.HP] += this[EStat.MaxHP] * 0.1f;
        if (this[EStat.HP] >= this[EStat.MaxHP])
        {
            this[EStat.HP] = this[EStat.MaxHP];
            tempHPBar.gameObject.SetActive(false);
        }
    }

    public void SetSpaceshipLoadedHP(float _Hp)
    {
        float savedHP = _Hp;
        if (savedHP <= 0f)
        {
            return;
        }

        this[EStat.HP] = savedHP;

        if (savedHP < this[EStat.MaxHP])
        {
            float persent = this[EStat.HP] / this[EStat.MaxHP];
            tempHPBar.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timeManager.dayChangeHealing.AddListener(HealAday);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            TakeDamage(10f);
        }
    }

}
