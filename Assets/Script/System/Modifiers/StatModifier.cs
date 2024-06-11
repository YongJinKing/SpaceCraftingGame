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
    [SerializeField]protected bool _isActive;
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

                if (_isActive && myStat != null)
                {
                    for(int i = 0; i < (int)EStat.Count; ++i)
                    {
                        if(dicModifiers.ContainsKey((EStat)i))
                        {
                            //별 상관없는 숫자를 더함으로써 Stat 클래스에 있는 이벤트를 발동시키기
                            //위함이다.
                            myStat[(EStat)i] += 0.0f;
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
        //Debug.Log($"Modifiers 전달 성공, 이름 : {gameObject.name}");
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
    private IEnumerator DelaiedLoad()
    {
        yield return new WaitForEndOfFrame();

        IGetStatValueModifier[] modifiers = GetComponentsInChildren<IGetStatValueModifier>();
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

    #region MonoBehaviour
    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Start()
    {
        /*
        List<ValueModifier> modifierList = new List<ValueModifier>();
        AddValueModifier add = new AddValueModifier(1, 20);
        MultValueModifier mult = new MultValueModifier(2, 1.5f);

        modifierList.Add(add);
        modifierList.Add(mult);

        dicModifiers.Add(E_BattleStat.ATK, modifierList);
        */

        myStat = GetComponentInParent<Stat>();

        StartCoroutine(DelaiedLoad());
    }
    #endregion
}