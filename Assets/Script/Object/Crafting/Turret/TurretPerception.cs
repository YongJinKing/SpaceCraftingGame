using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretPerception : MonoBehaviour
{
    #region Properties
    #region private
    private List<GameObject> detectedEnemies = new List<GameObject>();
    #endregion
    #region public
    public LayerMask enemyMask;
    #endregion
    #region events
    public UnityEvent<GameObject> detectEnemyEvents;
    public UnityEvent<GameObject> lostEnemyEvents;
    public UnityEvent destroyBulletEvents;
    #endregion
    #endregion

    #region TriggerEvents
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            detectedEnemies.Add(other.gameObject);
            detectEnemyEvents?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            //destroyBulletEvents?.Invoke();
            detectedEnemies.Remove(other.gameObject);
            lostEnemyEvents?.Invoke(other.gameObject);
        }
    }
    #endregion

    #region Method
    #region public
    public List<GameObject> GetDetectedEnemies()
    {
        return detectedEnemies;
    }
    #endregion
    #endregion
}
