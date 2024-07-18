using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitBossSuperJumpAttackAction : BossAction
{
    #region Properties
    #region Private
    Vector2 targetPos;
    
    #endregion
    #region Protected
    #endregion
    #region Public
    public GameObject fallPosImage;
    public Transform attackVFX;
    public float jumpHeight = 20f; // 토끼가 점프하는 높이
    public float delayBeforeFall = 2f; // 토끼가 떨어지기 전에 대기하는 시간
    public float gravity = -9.8f; // 중력 가속도
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossSuperJumpAttackAction()
    {
        fireAndForget = false;
        
    }
    #endregion

    #region Methods
    #region Private
    IEnumerator HitBoxOn(Vector2 pos)
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
        }

        attackVFX.GetComponent<ParticleSystem>().Play();
        yield return null;
    }

    IEnumerator SuperJumpingAttack()
    {
        Debug.Log("슈ㅠ퍼 점프 시작");
        Rigidbody2D rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        Transform rabbit = transform.parent.parent.transform;
        // AsyncAnimation(0, false);
        
        //yield return new WaitForSeconds(1f); // 일단은 1초 기다리고 점프 시작, 나중에는 애니메이션에 맞춰서 할듯

        // 초기 속도 설정
        float verticalSpeed = jumpHeight;

        // 화면 밖으로 올라가기
        while (rb.position.y < Camera.main.orthographicSize + 10)
        {
            verticalSpeed += gravity * Time.deltaTime; // 중력 가속도 적용

            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            rb.position = new Vector2(rb.position.x, rb.position.y + rb.velocity.y * Time.deltaTime);
            yield return null;
        }

        rb.velocity = Vector2.zero; // 속도 초기화

        // 플레이어 위치 표시
        targetPos = FindFirstObjectByType<Player>().gameObject.transform.position;

        rabbit.position = new Vector2(targetPos.x, Camera.main.orthographicSize + 10);
        GameObject obj = Instantiate(fallPosImage, targetPos, Quaternion.identity);
        // 위의 이미지는 켜지면 n초 뒤에 스스로 꺼지도록 간단하게 스크립트를 짠다.

        // 대기 시간
        yield return new WaitForSeconds(delayBeforeFall);

        // 중력 가속도로 플레이어 위치로 떨어지기
        ownerAnim.SetBool("SuperJumping", true);
        verticalSpeed = 0f; // 속도 초기화
        //AsyncAnimation(1, true);
        while (rabbit.position.y > targetPos.y)
        {
            verticalSpeed += gravity * Time.deltaTime * 2; // 중력 가속도 적용
            rabbit.position = new Vector2(rabbit.position.x, rabbit.position.y + verticalSpeed * Time.deltaTime);
            yield return null;
        }


        StartCoroutine(HitBoxOn(targetPos));
        // 토끼 위치를 플레이어 위치로 고정
        rabbit.position = targetPos;
        ownerAnim.SetBool("SuperJumping", false);
        //AsyncAnimation(2, false);
        rb.velocity = Vector2.zero; // 가해진 속도 초기화

    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        
        ownerAnim.SetTrigger("SuperJumpAttack");
    }

    public void StartSuperJump()
    {
        StartCoroutine(SuperJumpingAttack());
    }
    public override void Deactivate()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Deactivate();
        }
        
        ActionEnd();
    }


    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
