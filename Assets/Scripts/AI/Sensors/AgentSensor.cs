using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSensor : MonoBehaviour
{
    public BTController controller;

    public virtual bool hasTransform(Transform t) { return false; }
}
