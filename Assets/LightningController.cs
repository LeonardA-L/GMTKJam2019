using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public List<GameObject> m_lights;
    public float m_t1 = 0.3f;
    public float m_t2 = 0.2f;
    public float m_t3 = 0.4f;
    public float m_wait = 3.0f;
    public float m_timer = 0;
    public bool m_flicker = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_timer = 0;
        m_flicker = Random.Range(0, 5) < 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_flicker)
        {
                foreach (var item in m_lights)
                {
                    item.SetActive(m_timer < m_t1 + m_t2);
                }
        }
        else
        {
            if ((m_timer >= m_t1 && m_timer < m_t1 + m_t2) || m_timer > m_t1 + m_t2 + m_t3)
            {
                foreach (var item in m_lights)
                {
                    item.SetActive(false);
                }
            }
            else
            {
                foreach (var item in m_lights)
                {
                    item.SetActive(true);
                }
            }
        }
        m_timer += Time.deltaTime;
        if(m_timer > m_t1 + m_t2 + m_t3 + m_wait)
        {
            m_timer = 0;
            m_flicker = Random.Range(0, 5) < 3;
        }
    }
}
