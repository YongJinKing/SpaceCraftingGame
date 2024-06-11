using UnityEngine;

public enum ValueModifierType
{
    Add,Mult
}

public interface IGetStatValueModifier
{
    public Info<EStat, ValueModifier> GetStatValueModifier();
}

public class StatModifyFeature : MonoBehaviour, IGetStatValueModifier
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public EStat statType;
    public ValueModifierType modifierType = ValueModifierType.Add;
    public ValueModifier VM = null;
    public int sortOrder = 1;
    public float value = 0;
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
    public Info<EStat, ValueModifier> GetStatValueModifier()
    {
        /*
        Debug.Log($"Modifier 전달, 이름 : {gameObject.name}, 값 ${statType}, {value}");
        if(VM != null)
        {
            Debug.Log($"Feature 값 전달 성공");
        }
        */
        return new Info<EStat, ValueModifier>(statType, VM);
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        switch (modifierType)
        {
            case ValueModifierType.Add:
                {
                    VM = new AddValueModifier(sortOrder, value);
                }
                break;
            case ValueModifierType.Mult:
                {
                    VM = new MultValueModifier(sortOrder, value);
                }
                break;
        }
    }
    #endregion
}
