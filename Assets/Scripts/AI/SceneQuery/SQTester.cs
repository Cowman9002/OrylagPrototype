using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SQTester : MonoBehaviour
{
    private Gradient m_weightGradient;
    public SceneQuery sceneQuery;
    private Vector3 winningPosition;

    void Start()
    {
        if (m_weightGradient == null) CreateGradient();

        if (sceneQuery)
            sceneQuery.Evaluate(out winningPosition);
    }

    void Update()
    {
        if(sceneQuery)
            sceneQuery.Evaluate(out winningPosition);
    }

    private void OnDrawGizmosSelected()
    {
        if(sceneQuery)
        {
            if (m_weightGradient == null) CreateGradient();

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(sceneQuery.transform.position, new Vector3(sceneQuery.width, 0.5f, sceneQuery.height));

            foreach (SceneQuery.QueryPoint p in sceneQuery.queryPoints)
            {
                Gizmos.color = p.weight == 0.0f ? Color.blue : m_weightGradient.Evaluate(p.weight);
                Gizmos.DrawSphere(p.position, 0.5f);
                Gizmos.DrawWireSphere(p.position, 0.5f);

                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(winningPosition, 0.5f);
            }
        }
    }

    private void CreateGradient()
    {
        m_weightGradient = new Gradient();
        m_weightGradient.colorKeys = new GradientColorKey[]
           {
                new GradientColorKey(Color.red, 0.0f),
                new GradientColorKey(Color.yellow, 0.5f),
                new GradientColorKey(Color.green, 1.0f),
           };

        m_weightGradient.alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(0.6f, 0.0f),
            };
    }
}
