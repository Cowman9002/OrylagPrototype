using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSensor : AgentSensor
{
    private Dictionary<Transform, uint> oldObjects = new Dictionary<Transform, uint>();

    public override bool hasTransform(Transform t)
    {
        return oldObjects.ContainsKey(t);
    }

    private void OnTriggerEnter(Collider other)
    {
        uint count;
        if (oldObjects.TryGetValue(other.transform, out count))
        {
            oldObjects[other.transform] = count + 1;

        }
        else
        {
            oldObjects.Add(other.transform, 0);
            controller.SensorObjectEnter(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        uint count;
        if (oldObjects.TryGetValue(other.transform, out count))
        {
            if (count == 0)
            {
                oldObjects.Remove(other.transform);
                controller.SensorObjectExit(other.transform);
            }
            else
            {
                oldObjects[other.transform] = count - 1;
            }
        }
    }
}
