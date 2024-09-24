using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipmentManager : MonoBehaviour
{
    #region Properties
    #region Private
    private Player myPlayer;
    #endregion
    #region Protected
    protected Equipment[] equipment = new Equipment[(int)EEquipmentType.Count];
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent equipFailEvent = new UnityEvent();
    public UnityEvent equipSuccessEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public void EquipItem(Equipment equipment)
    {
        if (!myPlayer.canEquip)
        {
            equipFailEvent?.Invoke();
            return;
        }
            

        if (this.equipment[(int)equipment.itemType] != equipment)
        {
            if (this.equipment[(int)equipment.itemType] != null)
                this.equipment[(int)equipment.itemType].UnEquip();
            this.equipment[(int)equipment.itemType] = equipment;

            if(equipment != null)
            {
                if (equipment.animType > 0)
                    myPlayer.myAnim.SetEquipType(equipment.animType);

                equipment.transform.SetParent(myPlayer.transform, false);
                equipment.transform.localPosition = Vector3.zero;
                equipment.gameObject.SetActive(true);

                StartCoroutine(Equiping(equipment));
            }
        }
    }

    public void UnEquip(EEquipmentType type)
    {
        if (equipment[(int)type] != null)
        {
            equipment[(int)type].UnEquip();

            myPlayer.myAnim.SetEquipType(0);
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator Equiping(Equipment equipment)
    {
        yield return new WaitForEndOfFrame();
        equipment.Equip();
        equipSuccessEvent?.Invoke();
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        myPlayer = GetComponent<Player>();
    }
    #endregion
}
