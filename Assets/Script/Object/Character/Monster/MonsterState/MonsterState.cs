
public abstract class MonsterState : State
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Monster owner;
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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<Monster>();
    }
    #endregion
}