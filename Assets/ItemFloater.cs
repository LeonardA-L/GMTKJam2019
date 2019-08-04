using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloater : MonoBehaviour
{
    private Vector3 m_position;
    public float m_ampl = 1;
    public float m_speed = 0.3f;
    public Transform m_shadow = null;
    public float m_scaleAmpl = 1;
    public float m_scaleSpeed = 0.3f;
    public bool m_ZAxis = false;

    // Start is called before the first frame update
    void Awake()
    {
        m_position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_position;
        if (m_ZAxis)
        {
            pos += new Vector3(m_ampl * Mathf.Sin(Time.time * m_speed), 0, 0);
        }
        else
        {
            pos += new Vector3(0, m_ampl * Mathf.Sin(Time.time * m_speed), 0);
        }
        transform.localPosition = pos;

        if (m_shadow != null)
        {
            m_shadow.localScale = new Vector3(1, 1, 1) * (1 + m_scaleAmpl * Mathf.Sin(Time.time * m_scaleSpeed));
        }
    }
}
