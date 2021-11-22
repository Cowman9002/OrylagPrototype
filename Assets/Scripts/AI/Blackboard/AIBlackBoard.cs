using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlackBoard : MonoBehaviour
{
    private Dictionary<string, BlackBoardItem> m_data = new Dictionary<string, BlackBoardItem>();

    public BlackBoardItem.EType getItem(string name, out BlackBoardItem item)
    {
        if (!m_data.TryGetValue(name, out item)) return BlackBoardItem.EType.INVALID;
        if(item == null) return BlackBoardItem.EType.INVALID;


        return item.Type;
    }

    public void setItem(string name, BlackBoardItem item)
    {
        m_data[name] = item;
    }

    public void removeItem(string name)
    {
        m_data.Remove(name);
    }
}
