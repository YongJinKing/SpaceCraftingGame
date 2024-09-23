using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingProduceAmountUI : MonoBehaviour
{
    public GameObject produceUI;
    public Vector3 offset;
    [SerializeField] Text proudeAmountText;

    Vector3 screenPos;
    Structure target;
    int _produce;
    int _max;
    Coroutine following;
    public void ActiveAmountUI(Structure target)
    {
        produceUI.SetActive(true);
        this.target = target;
        following = StartCoroutine(Following());
        Debug.Log("스타트");   
    }

    public void DeActiveAmountUI()
    {
        if (following != null)
        {
            StopCoroutine(following);
            following = null;
        }
        produceUI.SetActive(false);
    }

    protected IEnumerator Following()
    {
        while (true)
        {
            screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            produceUI.transform.position = screenPos + offset;
            proudeAmountText.text = "현재 체력 : " + target[EStat.HP] + "/" + target[EStat.MaxHP];

            yield return null;
        }
    }
}
