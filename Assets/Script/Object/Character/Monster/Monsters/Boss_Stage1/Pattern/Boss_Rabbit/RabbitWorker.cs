using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �����뿡 ���� ���� �䳢���� ��ũ��Ʈ
// OnEnable �Ǹ� ���� �����ϴ� �ڷ�ƾ�� �����ϰ� Disable �ɶ� �ڷ�ƾ�� �����Ѵ�.
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
