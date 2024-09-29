using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region ForTest
    [SerializeField] protected GameObject WaveMonsterPrefab;
    [SerializeField] protected int monsterCount = 1;
    public float SpawnOffset = 30.0f;
    #endregion

    #region Properties
    #region Private
    private int priviousWaveDate = 0;
    public int waveCount = 0;
    #endregion
    #region Protected
    [SerializeField] protected int monsterWaveDay = 7;
    [SerializeField] protected float waveDifficultyScale = 1.5f;
    [SerializeField] protected HPBar hpBarPrefab;
    #endregion
    #region Public
    public TimeManager timeManager;
    public Transform MonsterHPUICanvas;
    #endregion
    #region Events

    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    protected void OnDayChanged()
    {
        if(timeManager.day - priviousWaveDate - monsterWaveDay >= 0)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.spaceShipWarning);
            priviousWaveDate = timeManager.day;
            StartCoroutine(ProcessingWave());
            waveCount++;
        }
    }
    #endregion

    #region Coroutines
    protected IEnumerator ProcessingWave()
    {
        int count = (int)(monsterCount * (waveCount * waveDifficultyScale)) + monsterCount;

        Debug.Log("GameManager.ProcessingWave");

        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(0.0f, 360.0f);

            //일단 임시로 한군데에서 스폰
            GameObject temp = Instantiate(WaveMonsterPrefab);
            HPBar tempHPBar = Instantiate(hpBarPrefab);
            tempHPBar.myTarget = temp.GetComponent<Stat>();

            tempHPBar.transform.SetParent(MonsterHPUICanvas, false);

            temp.transform.position = new Vector3(SpawnOffset * Mathf.Cos(angle * Mathf.Deg2Rad), SpawnOffset * Mathf.Sin(angle * Mathf.Deg2Rad), -0.1f);
            temp.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (timeManager != null)
        {
            Debug.Log("GameManager.start");
            timeManager.dayChangeEvent.AddListener(OnDayChanged);
        }
    }
    private void OnDestroy()
    {
        if(timeManager != null)
        {
            timeManager.dayChangeEvent.RemoveListener(OnDayChanged);
        }
    }
    #endregion
}
