using UnityEngine;

public interface IDataDisplay<T>
{
    public T Data();
    public void setDisplay(T data);
}
