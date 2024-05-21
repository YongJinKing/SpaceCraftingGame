using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected State _currentState;
    protected bool _inTransition;
    #endregion
    #region Public
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
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
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
    #endregion
    #region Public
    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }
    public virtual void ChangeState<T0, T1>(T1 info) where T0 : State
    {
        CurrentState = GetState<T0>();
        _currentState.GetInfo<T1>(info);
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}