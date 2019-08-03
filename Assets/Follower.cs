using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform m_toFollow = null;
    public float m_speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(m_toFollow != null);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = m_toFollow.position;
        pos.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, pos, m_speed);
    }
}
