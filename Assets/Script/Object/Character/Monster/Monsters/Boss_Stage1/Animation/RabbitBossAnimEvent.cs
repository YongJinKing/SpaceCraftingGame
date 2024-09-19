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
    public void JumpSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossJump);
    }
    public void JumpAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossJumpAttack);
    }

    public void SpinAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossSpinAttack);
    }
    public void StopSpinAttackSound()
    {
        SoundManager.Instance.StopSFX(SoundManager.Instance.bossSoundData.bossSpinAttack);
    }
    public void SuperJumpAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossSuperJumpAttack);
    }

    public void SuperJumpFallingSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.meteorRiceFall, false);
    }

    public void ThrowReadySound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossThrowReady);
    }
    public void ThrowAttackSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossThrowAttack);
    }

    public void EatRiceSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossEat);
    }

    public void BuffSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossBuff);
    }

    public void HitMortalBoxSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossHitMortalBox);
    }

    public void DeadSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossDead);
    }
    public void DeadImpactSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossDeadImpact);
    }
    public void FootStepSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.bossFootstep);
    }

    #endregion
}
