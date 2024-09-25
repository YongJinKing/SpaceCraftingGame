using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipLanding : MonoBehaviour
{
    public Transform explainUI;
    public float openTime;
    public float closeTime;

    public RectTransform rectTransform;
    SpaceShipOperator spacShipOperator;

    private void Start()
    {
        spacShipOperator = GetComponent<SpaceShipOperator>();
    }
    public void StartExplain()
    {
        explainUI.gameObject.SetActive(true);
        StartCoroutine(GrowHeight(0,800,openTime));
    }

    public void StopExplain()
    {
        StartCoroutine(CloseHeight(800, 0, closeTime));
    }

    private IEnumerator GrowHeight(float startHeight, float targetHeight, float duration)
    {
        float elapsedTime = 0f;
        float initialHeight = startHeight;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            // Mathf.SmoothStep로 천천히 시작해서 점점 빠르게 변화시키기
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newHeight = Mathf.SmoothStep(initialHeight, targetHeight, t);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
            yield return null;
        }

        // 정확한 최종 크기로 맞추기
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);
        if(startHeight <= 0f) spacShipOperator.OperatorExplain();
    }

    private IEnumerator CloseHeight(float startHeight, float targetHeight, float duration)
    {
        spacShipOperator.StopOperator();
        yield return StartCoroutine(GrowHeight(startHeight, targetHeight, duration));

        explainUI.gameObject.SetActive(false);
    }
}
