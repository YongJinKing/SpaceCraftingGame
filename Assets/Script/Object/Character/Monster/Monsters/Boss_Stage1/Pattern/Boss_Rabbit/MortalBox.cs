using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���� �����濡�� ���� ������(mortal box) ������ ���� ��ũ��Ʈ

public class MortalBox : MonoBehaviour
{
    [SerializeField] GameObject rabbitWorker;
    [SerializeField] int riceCakes;
    [SerializeField] int maxCakes;
    [SerializeField] float countSpeed = 2f;
    [SerializeField] float timer = 0;
    [SerializeField] float spawnTime;

    public ParticleSystem riceRainVFX;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        riceCakes = 0;
        maxCakes = 5;
        spawnTime = Random.Range(10f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rabbitWorker.activeSelf)
        {
            timer += Time.deltaTime;
            if(timer >= spawnTime)
            {
                timer = 0f;
                SpawnRabbitWorker();
            }
        }
    }

    public void SpawnRabbitWorker()
    {
        if (rabbitWorker != null)
        {
            rabbitWorker.SetActive(true);
            rabbitWorker.GetComponent<RabbitWorker>().StartWorking();
            StartProducingCake();
        }
    }

    public void PlayVFX()
    {
        riceRainVFX.Play();
    }

    public void StartProducingCake()
    {
        StartCoroutine(MakingCake());
    }

    public void StopProducingCake()
    {
        StopCoroutine(MakingCake());
    }

    IEnumerator MakingCake()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            riceCakes++;
            if (riceCakes >= maxCakes)
            {
                riceCakes = maxCakes;
                anim.SetTrigger("MakeFull");
                anim.SetBool("IsFull", true);
                break;
            }
            yield return new WaitForSeconds(countSpeed);
        }
    }

    public int GetRice()
    {
        return riceCakes;
    }

    public bool ReduceCake(int amount) // ������ ���� ���� �Ҹ��ϴ� �Լ�, ����(= ���Ͽ� �ʿ��� ������ �����뿡 �ִ� ���� ���� ��)�ϸ� true, �ƴϸ� false ����
    {
        if (riceCakes >= amount)
        {
            riceCakes -= amount;
            anim.SetBool("IsFull", false);
            StartCoroutine(MakingCake());
            return true;
        }
        return false;
    }
}
