using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LandingCameraWalk : MonoBehaviour
{
    public Transform CamFocus;
    public AnimationClip landingClip;
    public Transform StartPos;
    public Transform EndPos;
    public float camMoveSpeed;
    public FadeManager fadeManager;
    public SpaceShipLanding spaceLand;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = landingClip.length - 3f;
        StartCoroutine(LandingCamFocus());

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.PageDown))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("MainStage1");
        }*/
    }

    IEnumerator LandingCamFocus()
    {
        spaceLand.StartExplain();
        float elapsedTime = 0f;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.sacpeshipBoost);
        CamFocus.position = StartPos.position;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            CamFocus.position = Vector3.Lerp(StartPos.position, EndPos.position, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        SoundManager.Instance.StopSFX(SoundManager.Instance.spaceShipSondData.sacpeshipBoost);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.spaceshipLand);
        CamFocus.position = EndPos.position;
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.spaceshipChangeMode);
        yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(10f);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spaceShipSondData.spaceshipSearching);
        spaceLand.StopExplain();
        yield return new WaitForSeconds(2f);
        fadeManager.StartFadeOut(2f);
        yield return new WaitForSeconds(4f);
        Debug.Log("¾À ÀÌµ¿");
        AsyncOperation op = SceneManager.LoadSceneAsync("MainStage1");
    }

    
}
