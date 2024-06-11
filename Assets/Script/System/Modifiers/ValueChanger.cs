using System.Collections.Generic;

public class ValueChanger
{
    #region Properties
    #region Private
    private List<ValueModifier> modifiers;
    #endregion
    #region Protected
    #endregion
    #region Public
    public readonly float fromValue;
    public float delta { get { return GetModifiedValue() - fromValue; } }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public ValueChanger(float fromValue)
    {
        this.fromValue = fromValue;
    }
    #endregion

    #region Methods
    #region Private
    int Compare(ValueModifier x, ValueModifier y)
    {
        return x.sortOrder.CompareTo(y.sortOrder);
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    public void AddModifier(ValueModifier m)
    {
        if (modifiers == null)
            modifiers = new List<ValueModifier>();
        modifiers.Add(m);
    }

    public void AddModifiers(List<ValueModifier> modifiers)
    {
        if (this.modifiers == null)
            this.modifiers = new List<ValueModifier>();
        this.modifiers.AddRange(modifiers);
    }

    public float GetModifiedValue()
    {
        float value = fromValue;
        if (modifiers == null)
        {
            return value;
        }


        modifiers.Sort(Compare);
        for (int i = 0; i < modifiers.Count; ++i)
            value = modifiers[i].Modify(value);

        //UnityEngine.Debug.Log($"sccessed in VC value : {modifiers[1].sortOrder}");

        return value;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion
}