using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MortalBoxesSpawner : MonoBehaviour
{
    public MortalBox[] mortalBoxes;
    List<int> idxList = new List<int>() { 0, 1, 2, 3, 4 };
    int[] idxs = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        SelectIdx();
        
    }

    void SelectIdx()
    {
        for(int i = 0; i < 3; i++)
        {
            int idx = Random.Range(0, idxList.Count);
            idxs[i] = idxList[idx];
            idxList.RemoveAt(idx);
        }
        SpawnMortals();
    }

    void SpawnMortals()
    {
        mortalBoxes[idxs[0]].SpawnRabbitWorker();
        mortalBoxes[idxs[1]].SpawnRabbitWorker();
        mortalBoxes[idxs[2]].SpawnRabbitWorker();
    }
    
    public void StopAllMortals()
    {
        foreach(MortalBox mb in mortalBoxes)
        {
            if(mb.rabbitWorker.activeSelf) mb.rabbitWorker.GetComponent<RabbitWorker>().TakeDamage(10000);
            mb.endGame = true;
            mb.StopProducingCake();
        }
    }
}
