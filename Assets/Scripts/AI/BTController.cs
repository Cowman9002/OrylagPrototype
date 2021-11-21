using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTController : MonoBehaviour
{
    protected BTRoot root;

    protected Dictionary<string, Object> blackboard = new Dictionary<string, Object>();

    public bool getItemFromBB<T>(string name, out T item) where T : Object
    {
        Object obj;
        bool res = blackboard.TryGetValue(name, out obj);

        if(res)
        {
            item = (T)obj;
        }
        else
        {
            item = null;
        }

        return res;
    }

    public void removeItemFromBB(string name)
    {
        blackboard.Remove(name);
    }

    public void FixedUpdate()
    {
        root.Evaluate();
    }

    public virtual void SensorObjectEnter(Transform newObject) { }
    public virtual void SensorObjectExit(Transform newObject) { }
}
