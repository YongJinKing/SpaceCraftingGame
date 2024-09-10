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

    public Transform StartPos; // ���� ��, ���ʿ� ������
    public Transform EndPos; // ������, ���ʿ� ����
    public float jumpHeight = 2f; // ���� ����
    public float jumpDuration = 1f; // ���� �ð�

    public Animator animator;

    BoxCollider2D rabbitCollider;
    Coroutine working;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        rabbitCollider = GetComponent<BoxCollider2D>();
    }


    public void StartWorking()
    {
        rabbitCollider.enabled = true;
        if(working == null) working = StartCoroutine(JumpBetweenPoints());
    }

    public void OnDead()
    {
        hp = maxHp;
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            rabbitCollider.enabled = false;
            animator.SetTrigger("Dead");
            mortalBox.StopProducingCake();
            if(working != null) StopCoroutine(working);
            working = null;
        }
    }

    private IEnumerator JumpBetweenPoints()
    {
        Vector3 startPoint = StartPos.position;
        Vector3 endPoint = EndPos.position;

        while (true)
        {
            yield return Jump(startPoint, endPoint);
            yield return Jump(endPoint, startPoint);
        }
    }

    private IEnumerator Jump(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            // �������� �׸��� �����ϴ� ����
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
            Vector3 currentPosition = Vector3.Lerp(start, end, t);
            currentPosition.y += height;

            transform.position = currentPosition;

            yield return null;
        }

        transform.position = end;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }
}
