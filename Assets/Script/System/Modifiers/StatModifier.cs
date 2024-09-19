using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IGetStatValueModifiers
{
    public List<ValueModifier> GetStatValueModifiers(EStat statType);
}

public class StatModifier : MonoBehaviour, IGetStatValueModifiers
{
    #region Properties
    #region Private
    private Stat myStat;
    #endregion
    #region Protected
    protected Dictionary<EStat, List<ValueModifier>> dicModifiers = new Dictionary<EStat, List<ValueModifier>>();
    [SerializeField]protected bool _isActive = false;
    #endregion
    #region Public
    public bool isActive
    {
        get { return _isActive; }
        set 
        { 
            if(_isActive != value)
            {
                _isActive = value;

                if (myStat != null)
                {
                    for(int i = 0; i < (int)EStat.Count; ++i)
                    {
                        if(dicModifiers.ContainsKey((EStat)i))
                        {
                            //별 상관없는 숫자를 더함으로써 Stat 클래스에 있는 이벤트를 발동시키기
                            //위함이다.
                            myStat[(EStat)i] = myStat.GetRawStat((EStat)i) + 0.0f;
                        }
                    }
                }
            }
        }
    }
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
    public List<ValueModifier> GetStatValueModifiers(EStat statType)
    {
        /*
        Debug.Log($"Modifiers 전달 성공, 이름 : {gameObject.name}");
        foreach(List<ValueModifier> temp in dicModifiers.Values)
        {
            Debug.Log(temp.Count);
        }
        */
        if (_isActive && dicModifiers.ContainsKey(statType))
            return dicModifiers[statType];
        else
            return null;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable() 
    {
        isActive = false; 
    }

    private void OnDestroy()
    {
        isActive = false;
    }

    private void Start()
    {
        myStat = GetComponentInParent<Stat>();

        IGetStatValueModifier[] modifiers = GetComponentsInChildren<IGetStatValueModifier>();

        //Debug.Log($"modifiers.Length : {modifiers.Length} myStat.GameObject {myStat.gameObject.name}");

        Info<EStat, ValueModifier> temp;
        foreach (IGetStatValueModifier data in modifiers)
        {
            temp = data.GetStatValueModifier();
            if (temp.arg1 != null)
            {
                List<ValueModifier> tempList;
                if (dicModifiers.ContainsKey(temp.arg0))
                {
                    tempList = dicModifiers[temp.arg0];
                    tempList.Add(temp.arg1);
                }
                else
                {
                    tempList = new List<ValueModifier>();
                    tempList.Add(temp.arg1);
                    dicModifiers.Add(temp.arg0, tempList);
                }
            }
        }
    }
    #endregion
}