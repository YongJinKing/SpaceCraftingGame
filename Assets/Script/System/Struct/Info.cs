public struct Info<T0>
{
    public T0 arg0;
    public Info(T0 arg0)
    {
        this.arg0 = arg0;
    }
}

public struct Info<T0, T1>
{
    public T0 arg0;
    public T1 arg1;
    public Info(T0 arg0, T1 arg1)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}

public struct Info<T0, T1, T2>
{
    public T0 arg0;
    public T1 arg1;
    public T2 arg2;
    public Info(T0 arg0, T1 arg1, T2 arg2)
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
        this.arg2 = arg2;
    }
}