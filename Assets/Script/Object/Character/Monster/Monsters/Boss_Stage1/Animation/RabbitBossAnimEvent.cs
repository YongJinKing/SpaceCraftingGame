using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RabbitBossAnimEvent : MonoBehaviour
{
    public UnityEvent ThrowEvent;
    public UnityEvent ThrownEndEvent;
    public UnityEvent SuperJumpEvent;
    public UnityEvent SpinningEvent;
    public UnityEvent RiceRainEvent;
    public UnityEvent BuffEvent;
    public UnityEvent DeadEvent;

    public UnityEvent<float, float> CamShakeEvent;

    #region AnimEvent
    public void ThrowAnimation()
    {
        ThrowEvent?.Invoke();
    }

    public void ThrowEndAnimation()
    {
        ThrownEndEvent?.Invoke();
    }

    public void SuperJumpAnimation()
    {
        SuperJumpEvent?.Invoke();
    }

    public void SpinningAnimation()
    {
        SpinningEvent?.Invoke();
    }

    public void RiceRainAnimation()
    {
        RiceRainEvent?.Invoke();
    }

    public void BuffAnimation()
    {
        BuffEvent?.Invoke();
    }

    public void DeadAnimation()
    {
        DeadEvent?.Invoke();
    }
    #endregion

    public void CamShakeAnimation()
    {
        CamShakeEvent?.Invoke(7f, 0.75f);
    }

    #region Sound

    public void JumpAttackSound()
    {
        Debug.Log("점프 공격 사운드 출력?");
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossJumpAttack);
    }

    public void SpinAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossSpinAttack);
    }

    public void SuperJumpAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossSuperJumpAttack);
    }

    public void ThrowAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossThrowAttack);
    }

    public void FootStepSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossFootstep);
    }

    #endregion
}
