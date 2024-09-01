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
    GameObject target;
    int _produce;
    int _max;
    Coroutine following;
    public void ActiveAmountUI(GameObject target, int produce, int max)
    {
        produceUI.SetActive(true);
        this.target = target;
        _produce = produce;
        _max = max;
        following = StartCoroutine(Following());
        
    }

    public void DeActiveAmountUI()
    {
        StopCoroutine(following);
        produceUI.SetActive(false);
    }

    protected IEnumerator Following()
    {
        while (true)
        {
            screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            produceUI.transform.position = screenPos + offset;
            proudeAmountText.text = "»ý»ê·®\n" + _produce.ToString() + " / " + _max.ToString();

            yield return null;
        }
    }
}
