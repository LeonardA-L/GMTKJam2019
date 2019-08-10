using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollerSingle : MonoBehaviour
{
    public List<string> m_name = null;
    public List<GameObject> m_toTej = null;
    public List<GameObject> m_toAct = null;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var item in m_toTej)
        {
            item.SetActive(true);
        }
        foreach (var item in m_toAct)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool RES = true;
        foreach (var item in m_name)
        {
            RES &= ProgressionManager.Instance.Get(item);
        }
        if (RES)
        {
            foreach (var item in m_toTej)
            {
                item.SetActive(false);
            }
            foreach (var item in m_toAct)
            {
                item.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
