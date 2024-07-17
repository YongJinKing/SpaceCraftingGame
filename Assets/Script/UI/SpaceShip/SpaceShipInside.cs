using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShipInside : MonoBehaviour
{

    public GameObject CraftingBox;
    public GameObject Screen;
    public GameObject GoButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        CraftingBox.GetComponent<RectTransform>().localScale = new Vector2(120, 100);
    }
    private void OnMouseExit()
    {
        
    }



}
