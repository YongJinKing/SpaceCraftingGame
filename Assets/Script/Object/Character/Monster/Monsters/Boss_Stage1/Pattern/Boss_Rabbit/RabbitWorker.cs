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

    public Transform StartPos; // 시작 점, 최초엔 오른쪽
    public Transform EndPos; // 착지점, 최초엔 왼쪽
    public float jumpHeight = 2f; // 점프 높이
    public float jumpDuration = 1f; // 점프 시간

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

            // 포물선을 그리며 점프하는 로직
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
