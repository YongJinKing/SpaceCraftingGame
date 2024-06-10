using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 절구통에 떡을 찧는 토끼들의 스크립트
// OnEnable 되면 떡을 생산하는 코루틴을 시작하고 Disable 될때 코루틴을 종료한다.
public class RabbitWorker : MonoBehaviour
{
    [SerializeField] MortalBox mortalBox;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        mortalBox.StartProducingCake();
    }

    private void OnDisable()
    {
        mortalBox.StopProducingCake();
    }



}
