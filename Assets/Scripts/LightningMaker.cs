using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMaker : MonoBehaviour
{
    public float m_range = 2;
    public float m_base = 3;
    public float m_timer = 0;
    public GameObject m_lightning;
    // Start is called before the first frame update
    void Start()
    {
        m_timer = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_timer > 0)
        {
            m_timer -= Time.deltaTime;
        }
        if(m_timer <= 0)
        {
            m_lightning.SetActive(true);
            PickRand();
        }
    }

    private void PickRand()
    {
        m_timer = Random.Range(m_base, m_base + m_range);
    }
}
