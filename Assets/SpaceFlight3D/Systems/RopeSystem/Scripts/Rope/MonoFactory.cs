using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFactory<T> : MonoBehaviour
    where T: MonoBehaviour
{ 
    protected virtual T Spawn(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        OnInstantiationBegin();
        T instance = Instantiate(prefab, position, rotation, parent);
        OnInstantiationCompleted(instance);
        return instance;
    }

    protected virtual void OnInstantiationBegin() { }
    protected virtual void OnInstantiationCompleted(T instance) { }
}
