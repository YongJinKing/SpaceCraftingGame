using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MortalBoxGage : MonoBehaviour
{
    #region Properties
    #region Private
    private Slider _mySlider;
    [SerializeField] private MortalBox _myTarget;
    #endregion
    #region Protected
    [SerializeField] protected Vector2 _offSet;
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
    private void Initialize()
    {
        AddListeners();
        _mySlider = GetComponent<Slider>();
        _mySlider.value = 0.0f;
        StartCoroutine(Following());
    }

    #endregion
    #region Protected
    protected void AddListeners()
    {
        if (_myTarget != null)
        {
            _myTarget.AddRiceEventListener(OnChangedCakes);
        }
    }
    protected void RemoveListeners()
    {
        if (_myTarget != null)
        {
            
        }
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnChangedCakes(float riceCake)
    {
        _mySlider.value = riceCake;
        //Debug.Log("Changed " + _mySlider.value);
    }
    #endregion

    #region Coroutines
    protected IEnumerator Following()
    {
        Vector2 screenPos;
        while (true)
        {
            screenPos = Camera.main.WorldToScreenPoint(_myTarget.transform.position);
            transform.position = screenPos + _offSet;

            yield return null;
        }
    }
    
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Initialize();
    }
    
    #endregion
}
