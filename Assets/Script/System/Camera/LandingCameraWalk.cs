using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingCameraWalk : MonoBehaviour
{
    public Transform CamFocus;
    public AnimationClip landingClip;
    public Transform StartPos;
    public Transform EndPos;
    public float camMoveSpeed;
    public FadeManager fadeManager;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = landingClip.length - 3f;
        StartCoroutine(LandingCamFocus());

    }

    IEnumerator LandingCamFocus()
    {
        float elapsedTime = 0f;

        CamFocus.position = StartPos.position;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            CamFocus.position = Vector3.Lerp(StartPos.position, EndPos.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        CamFocus.position = EndPos.position;

        yield return new WaitForSeconds(3f);
        fadeManager.StartFadeIn(2f);
        yield return new WaitForSeconds(2.5f);
        Debug.Log("¾À ÀÌµ¿");
    }

    
}
