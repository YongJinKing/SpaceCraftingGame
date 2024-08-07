using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return;

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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator Equiping(Equipment equipment)
    {
        yield return new WaitForEndOfFrame();
        equipment.Equip();
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        myPlayer = GetComponent<Player>();
    }
    #endregion
}
