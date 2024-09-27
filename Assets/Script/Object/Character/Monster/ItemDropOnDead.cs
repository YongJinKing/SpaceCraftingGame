using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropOnDead : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public int amount = 1;
    public GameObject dropItem;
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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Stat owner = GetComponentInParent<Stat>();

        if(owner != null)
        {
            owner.deadEvent.AddListener(() =>
            {
                if (dropItem == null)
                    return;

                for (int i = 0; i < amount; i++)
                {
                    Instantiate(dropItem, this.transform.position, Quaternion.identity, null);
                }
            });
        }
    }
    #endregion
}
