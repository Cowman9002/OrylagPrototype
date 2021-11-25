using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBouncer : MonoBehaviour
{
    public float bounceSpeed;
    public float spinSpeed;

    public float bounceAmplitude;

    private float m_yaw;
    private float m_startY;

    private void Start()
    {
        m_startY = transform.position.y;
    }

    void Update()
    {
        Vector3 posWorkspace = transform.position;

        posWorkspace.y = m_startY + Mathf.Sin(Time.time * bounceSpeed) * bounceAmplitude;
        m_yaw += Time.deltaTime * spinSpeed;

        transform.position = posWorkspace;
        transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up);
    }
}
