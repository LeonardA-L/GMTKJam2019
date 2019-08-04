using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : Singleton<ProgressionManager>
{
    private Dictionary<string, bool> m_switches = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Awake()
    {
        m_switches.Clear();
    }

    public void Set(string name, bool value)
    {
        m_switches[name] = value;
    }

    public bool Get(string name)
    {
        bool value = false;
        m_switches.TryGetValue(name, out value);
        return value;
    }
}
