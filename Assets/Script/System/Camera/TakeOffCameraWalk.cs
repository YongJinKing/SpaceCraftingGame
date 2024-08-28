using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffCameraWalk : MonoBehaviour
{
    public FadeManager fadeManager;
    public Animator SpaceShipAnimator;
    public AnimationClip takeOffClip;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = takeOffClip.length;
        StartCoroutine(StartTakeOff());

    }

    void SetTakeOff()
    {
        SpaceShipAnimator.SetTrigger("TakeOff");
    }

    IEnumerator StartTakeOff()
    {
        fadeManager.StartFadeIn(1.5f);
        yield return new WaitForSeconds(1.5f);
        SetTakeOff();
        yield return new WaitForSeconds(time - 4f);
        fadeManager.StartFadeOut(1.5f);
    }

    
}
