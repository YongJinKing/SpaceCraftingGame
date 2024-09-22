using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIWarningSign : MonoBehaviour
{
    [Header("해당 위치에 지을 수 없다는걸 알려줄 경고 문구"), Space(0.5f)]
    [SerializeField] GameObject buildWarning_CantBuildThere;

    [Header("건축 재료가 부족한걸 알려줄 경고 문구"), Space(0.5f)]
    [SerializeField] GameObject buildWarning_NoMoreResources;

    public CraftBuildingManager craftManager;
    public GameObject UISoundManager;
    private void Start()
    {
        craftManager.ActiveCantBuildThere.AddListener(TurnOnCantBuildThere);
        CraftFactory.Instance.ActiveNoMoreResources.AddListener(TurnOnNoMoreResources);
    }

    public void TurnOnCantBuildThere()
    {
        if(!buildWarning_CantBuildThere.activeSelf) buildWarning_CantBuildThere.SetActive(true);
        UISoundManager.GetComponent<UISoundPlayer>().NeedMoreResource();
    }

    public void TurnOnNoMoreResources()
    {
        if (!buildWarning_NoMoreResources.activeSelf) buildWarning_NoMoreResources.SetActive(true);
        UISoundManager.GetComponent<UISoundPlayer>().NeedMoreResource();
    }
}
