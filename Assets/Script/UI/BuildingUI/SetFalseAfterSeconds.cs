using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFalseAfterSeconds : MonoBehaviour
{
    public float seconds = 2f;

    void OnEnable()
    {
        Invoke("DisableText", seconds);
        StartCoroutine(ShakeText());
    }

    void DisableText()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ShakeText()
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;
        float duration = 0.5f; // Èçµå´Â ½Ã°£
        float magnitude = 5f;  // Èçµå´Â ¼¼±â

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
