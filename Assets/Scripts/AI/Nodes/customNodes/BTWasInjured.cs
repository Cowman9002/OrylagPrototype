using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWasInjured : BTNode
{
    string m_source;
    int m_lastHealth;

    public BTWasInjured(string name, string injurySource) : base(name)
    {
        m_source = injurySource;
    }

    public override BTController.BTStateEndData Evaluate()  
    {
        if(controller.healthSelf == null)
        {
            Debug.LogError("Was Injured node used but no health component was found for ai");
            return controller.EndState(BTResult.Failure);
        }

        if(controller.healthSelf.CurrentHealth < m_lastHealth && controller.healthSelf.LastInjuryCause != null && controller.healthSelf.LastInjuryCause == m_source)
        {
            controller.healthSelf.ClearLastInjury();
            m_lastHealth = controller.healthSelf.CurrentHealth;
            return controller.EndState(BTResult.Success);
        }

        return controller.EndState(BTResult.Failure);
    }
}
