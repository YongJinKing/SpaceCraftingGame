using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image image;
    public GameObject selectImage;
    public GameObject SlotSoundManager;

    public void Select()
    {
        selectImage.SetActive(true);
        SlotSoundManager.GetComponent<UISoundPlayer>().ChangeWeapon();
    }

    public void UnSelect()
    {
        selectImage.SetActive(false);
    }

    private void Start()
    {
        selectImage.SetActive(false);
    }
}
