using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAnimate : BTNode
{
    private Animator m_anim;
    private string m_param;

    public BTAnimate(string name, Animator anim, string param) : base(name)
    {
        m_anim = anim;
        m_param = param;
    }

    public override BTController.BTStateEndData Evaluate()
    {
        m_anim.SetTrigger(m_param);

        return controller.EndState(BTResult.Success);
    }
}
