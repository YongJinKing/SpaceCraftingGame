using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    // Start is called before the first frame update
   public void NeedMoreResource()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.NeedMoreResourceSFX, true);
    }
    public void EnterSpaceship()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.EnterSpaceShipSFX, true);
    }
    public void ExitSpaceship()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.ExitSpaceShipSFX, true);
    }
    public void Hammering()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.HammeringSFX, true);
    }
    public void Mining()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.MiningSFX, true);
    }
    public void ChangeTool()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.ChangeToolSFX, true);
    }
    public void ChangeWeapon()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.ChangeWeaponSFX, true);
    }
    public void ChangeTab()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.ChangeTabSFX, true);
    }
    public void OpenTab()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.OpenTabSFX, true);
    }
    public void CloseTab()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.CloseTabSFX, true);
    }
    public void Building()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.BuildingSFX, true);
    }
    public void Sucess()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.SuccesSFX, true);
    }
}
