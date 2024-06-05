using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �����뿡 ���� ���� �䳢���� ��ũ��Ʈ
// OnEnable �Ǹ� ���� �����ϴ� �ڷ�ƾ�� �����ϰ� Disable �ɶ� �ڷ�ƾ�� �����Ѵ�.
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
