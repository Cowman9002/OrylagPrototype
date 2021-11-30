using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlaySound : BTNode
{
    private AudioSource m_source;

    public BTPlaySound(string name, AudioSource source) : base(name)
    {
        m_source = source;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        if(!m_source.isPlaying)
        {
            m_source.Play();
        }

        return controller.EndState(BTResult.Success);
    }
}
