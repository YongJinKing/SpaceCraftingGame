using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 절구통에 떡을 찧는 토끼들의 스크립트
// OnEnable 되면 떡을 생산하는 코루틴을 시작하고 Disable 될때 코루틴을 종료한다.
public class RabbitWorker : MonoBehaviour, IDamage
{
    [SerializeField] MortalBox mortalBox;
    [SerializeField] float maxHp;
    [SerializeField] float hp;

    public Animator animator;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWorking()
    {
        //animator.SetBool("Working", true);
    }

    public void DeadAnim()
    {
        //animator.SetTrigger("Dead");
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            //animator.SetBool("Working", false);
            //animator.SetTrigger("Dead");
            mortalBox.StopProducingCake();
        }

        hp = maxHp;
    }
}
