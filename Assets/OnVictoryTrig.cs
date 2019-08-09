using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVictoryTrig : MonoBehaviour
{
    public List<Light> m_lights = null;
    public float m_timer = 1f;
    public float m_goalIntensity = 2.53f;

    // Start is called before the first frame update
    void Start()
    {
        m_goalIntensity = m_lights[0].intensity;

        foreach (var item in m_lights)
        {
            item.intensity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            StartCoroutine(OnVictory());
        }
    }

    private IEnumerator OnVictory()
    {
        float t = m_timer;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            foreach (var item in m_lights)
            {
                item.intensity = Mathf.Lerp(0, m_goalIntensity, 1 - (t / m_timer));
            }
            yield return null;
        }
        InteractibleUIController.Instance.OpenVictory();
    }
}
