using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SQTester : MonoBehaviour
{
    public Gradient weightGradient;

    public SceneQuery sceneQuery;

    private Vector3 winningPosition;

    void Start()
    {
        sceneQuery.Evaluate(out winningPosition);
    }

    void Update()
    {
        sceneQuery.Evaluate(out winningPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(sceneQuery.transform.position, new Vector3(sceneQuery.width, 0.5f, sceneQuery.height));

        foreach (SceneQuery.QueryPoint p in sceneQuery.queryPoints)
        {
            Gizmos.color = p.weight == 0.0f ? Color.blue : weightGradient.Evaluate(p.weight);
            Gizmos.DrawSphere(p.position, 0.5f);
            Gizmos.DrawWireSphere(p.position, 0.5f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(winningPosition, 0.5f);
        }
    }
}
