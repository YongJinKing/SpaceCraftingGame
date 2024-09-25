using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Player player;
    public GameObject GameOverPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindingPlayer());
    }

    IEnumerator FindingPlayer()
    {
        int count = 0;
        while (player == null) 
        {
            player = FindObjectOfType<Player>();
            count++;
            if(count > 5)
                yield break;

            yield return new WaitForSeconds(1.0f);
        }

        player.deadEvent.AddListener(() => { StartCoroutine(PlayerDead()); });
    }

    IEnumerator PlayerDead() 
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(GameOverPrefab);
    }
}
