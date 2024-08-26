using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup CG;

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }
    
    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    // ���̵� �� �Լ�
    IEnumerator FadeIn(float duration)
    {
        float startAlpha = CG.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            CG.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / duration);
            yield return null;
        }
        CG.alpha = 1f; 
    }

    // ���̵� �ƿ� �Լ�
    IEnumerator FadeOut(float duration)
    {
        float startAlpha = CG.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            CG.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            yield return null;
        }
        CG.alpha = 0f;
    }
}
