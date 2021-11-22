using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQTest : SceneQuery
{
    public float nearRange;
    public float farRange;

    void Start()
    {
        nodes.Add(new SQInRangeNode(this, new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), nearRange, true));
        nodes.Add(new SQInRangeNode(this, new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), farRange, false));
        nodes.Add(new SQDistanceNode(this, new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), false));
        nodes.Add(new SQDistanceNode(this, new AIBlackBoard.BlackBoardElement(null, AIBlackBoard.BlackBoardElement.ElementType.Transform), true));
        nodes.Add(new SQDotProduct(this, new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), false));
        nodes.Add(new SQVisibleNode(this, new AIBlackBoard.BlackBoardElement("Target", AIBlackBoard.BlackBoardElement.ElementType.Transform), 0.5f, false));
    }
}
