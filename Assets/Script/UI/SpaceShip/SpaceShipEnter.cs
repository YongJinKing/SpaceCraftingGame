using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShipEnter : MonoBehaviour
{
    public LayerMask spaceshipLayer;
    public GameObject spaceShipCanvas;
    public GameObject invenCanvas;
    public GameObject buildingCanvas;
    public GameObject pauseCanvas;
    public Button exitButton;

    public UnityEvent UIEnterEvent = new UnityEvent();
    public UnityEvent UIExitEvent = new UnityEvent();

    public Material origin;
    public Material mouseEnter;

    public MeshRenderer meshRender;
    public GameObject UIsoundManger;
    
    public FadeManager fadeManager;
    public TotalSaveManager totalSaveManager;
    BuildingProduceAmountUI buildingInfoUI;
    SpaceShip spaceShip;
    Player player;
    float hpUITime;
    private void Start()
    {
        if(exitButton == null)
        {
            Debug.LogError("SpaceShipEnter class needs exitButton");
        }
        else
        {
            exitButton.onClick.AddListener(() => { UIExitEvent?.Invoke(); });
        }
        spaceShip = FindObjectOfType<SpaceShip>();
        buildingInfoUI = FindObjectOfType<BuildingProduceAmountUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spaceShipCanvas.activeSelf) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, spaceshipLayer);

        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & spaceshipLayer) != 0)
            {
                hpUITime += Time.deltaTime;
                //spaceShipCanvas.SetActive(true);
                meshRender.material = mouseEnter;
            }
            else
            {
                meshRender.material = origin;
                
            }
        }
        else
        {
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (invenCanvas.activeSelf || buildingCanvas.activeSelf || pauseCanvas.activeSelf || player.modeType == 0) return;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, spaceshipLayer);

            if (hit.collider != null)
            {
                if (((1 << hit.collider.gameObject.layer) & spaceshipLayer) != 0)
                {
                    UIEnterEvent?.Invoke();
                    spaceShipCanvas.SetActive(true);
                    UIsoundManger.GetComponent<UISoundPlayer>().EnterSpaceship();
                }
            }
        }
    }

    public void GoOutPlanet()
    {
        spaceShipCanvas.SetActive(false);
        fadeManager.StartFadeOut(3);
        totalSaveManager.SaveAll();

        Invoke("ChangeSceneToStageSelect", 5f);
    }

    void ChangeSceneToStageSelect()
    {
        SceneManager.LoadScene("SpaceShipTakeOffScene");
    }

    
}
