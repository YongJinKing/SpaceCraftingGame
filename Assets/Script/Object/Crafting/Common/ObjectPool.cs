using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public Dictionary<string, Stack<GameObject>> myPool = new Dictionary<string, Stack<GameObject>>();
    private void Awake()
    {
        base.Initialize();
    }

    public GameObject GetObject<T>(GameObject org, Transform p)
    {
        string Key = typeof(T).ToString();
        if (myPool.ContainsKey(Key))
        {
            if (myPool[Key].Count > 0)
            {
                GameObject obj = myPool[Key].Pop();
                obj.SetActive(true);
                obj.transform.SetParent(p);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.rotation = p.rotation;
                return obj;
            }
        }
        return Instantiate(org, p);
    }

    public GameObject GetObject(string Key, GameObject org, Transform p)
    {
        if (myPool.ContainsKey(Key))
        {
            if (myPool[Key].Count > 0)
            {
                GameObject obj = myPool[Key].Pop();
                obj.SetActive(true);
                obj.transform.SetParent(p);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.rotation = p.rotation;
                return obj;
            }
        }
        return Instantiate(org, p);
    }

    public void ReleaseObject<T>(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        string Key = typeof(T).ToString();

        if (!myPool.ContainsKey(Key))
        {
            myPool[Key] = new Stack<GameObject>();
        }

        myPool[Key].Push(obj);
    }
    public void ReleaseObject(string Key, GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);

        if (!myPool.ContainsKey(Key))
        {
            myPool[Key] = new Stack<GameObject>();
        }

        myPool[Key].Push(obj);
    }
}
