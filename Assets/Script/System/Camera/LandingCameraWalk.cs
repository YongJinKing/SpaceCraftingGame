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
    }

    
}
