using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ForTest
    [SerializeField] protected GameObject WaveMonsterPrefab;
    [SerializeField] protected int monsterCount = 1;
    public Vector2 SpawnPoint = new Vector2(30, 0);
    #endregion

    #region Properties
    #region Private
    private int priviousWaveDate = 0;
    private int waveCount = 0;
    #endregion
    #region Protected
    [SerializeField] protected int monsterWaveDay = 7;
    [SerializeField] protected float waveDifficultyScale = 1.5f;
    #endregion
    #region Public
    public TimeManager timeManager;
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
            //일단 임시로 한군데에서 스폰
            GameObject temp = Instantiate(WaveMonsterPrefab);
            temp.transform.position = new Vector3(SpawnPoint.x, SpawnPoint.y, -0.1f);
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
