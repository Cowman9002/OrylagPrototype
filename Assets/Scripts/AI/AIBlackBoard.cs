using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlackBoard : MonoBehaviour
{
    public struct BlackBoardElement
    {
        public enum ElementType { Transform, NavmeshAgent};
        public string key;
        public ElementType type;

        public BlackBoardElement(string key, ElementType type)
        {
            this.key = key;
            this.type = type;
        }
    }

    private Dictionary<string, Object> m_data = new Dictionary<string, Object>();

    public bool getItem<T>(string name, out T item) where T : Object
    {
        item = null;
        Object obj;
        bool res = m_data.TryGetValue(name, out obj);

        if (obj == null || res == false) return false;

        item = (T)obj;
        return true;
    }

    public void addItem(string name, Object obj)
    {
        m_data[name] = obj;
    }

    public void removeItem(string name)
    {
        m_data.Remove(name);
    }
}
