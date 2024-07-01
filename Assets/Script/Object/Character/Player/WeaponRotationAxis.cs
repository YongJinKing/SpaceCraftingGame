using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationAxis : MonoBehaviour
{
    #region Properties
    #region Private
    private Transform parent;
    private Coroutine followingMouse;
    private Vector2 mousePos;
    #endregion
    #region Protected
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
    private void FollowMousePos()
    {
        if (followingMouse != null)
        {
            StopCoroutine(followingMouse);
            followingMouse = null;
        }
        followingMouse = StartCoroutine(FollowingMousePos());
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnGetMousePos(Vector2 mousePos)
    {
        this.mousePos = mousePos;
    }
    #endregion

    #region Coroutines
    private IEnumerator FollowingMousePos()
    {
        yield return null;
        //parent의 x 스케일이 마이너스일때는 right 기준
        //parent의 x 스케일이 플러스 일때는 left 기준

        float scale;
        float dir;
        while (true)
        {
            scale = 1.0f;
            dir = 1.0f;

            if (parent.localScale.x > 0)
                scale = -1.0f;
            if (Vector2.Dot(scale * transform.up, mousePos - (Vector2)transform.position) < 0.0f) 
                dir = -1.0f;

            float angle = Vector2.Angle(scale * transform.right, mousePos - (Vector2)transform.position) * dir;
            transform.Rotate(Vector3.forward * angle, Space.World);

            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        FollowMousePos();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        parent = transform.parent;
        InputController.Instance.getMousePosEvent.AddListener(OnGetMousePos);
        FollowMousePos();
    }
    #endregion
}
