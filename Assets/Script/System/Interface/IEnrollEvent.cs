using UnityEngine.Events;

public interface IEnrollEvent
{
    public void Enroll(UnityAction action);
}

public interface IEnrollEvent<T>
{
    public void Enroll(UnityAction<T> action);
}

public interface IEnrollEvent<T0, T1>
{
    public void Enroll(UnityAction<T0, T1> action);
}