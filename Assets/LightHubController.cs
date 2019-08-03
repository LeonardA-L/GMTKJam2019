using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHubController : MonoBehaviour
{
    public bool m_available = false;
    public bool m_on = false;
    public bool m_hasBulb = false;
    public bool m_broken = false;
    public Light m_light = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_available = false;
        m_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_available && Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character")
        {
            m_available = true;
            UpdateTooltips();
        }
    }

    private void UpdateTooltips()
    {
        CanvasController.Instance.RemoveTooltips();
        if (m_hasBulb)
        {
            CanvasController.Instance.TooltipRemove();
        }
        else if (LightBulbManager.Instance.HasBulb && !m_broken)
        {
            CanvasController.Instance.TooltipPlace();
        }
        else if (LightBulbManager.Instance.HasBulb && m_broken)
        {
            CanvasController.Instance.TooltipBroken();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            m_available = false;
            CanvasController.Instance.RemoveTooltips();
        }
    }

    private void Toggle()
    {
        if (m_hasBulb)
        {
            LightBulbManager.Instance.ReleaseBulb();
            m_hasBulb = false;
            ShutDown();
        }
        else if (LightBulbManager.Instance.HasBulb && !m_broken)
        {
            m_hasBulb = true;
            LightBulbManager.Instance.TakeBulb();
            TurnOn();
        }
        UpdateTooltips();
    }

    private void TurnOn()
    {
        m_on = true;
        m_light.gameObject.SetActive(m_on);
    }

    private void ShutDown()
    {
        m_on = false;
        m_light.gameObject.SetActive(m_on);
    }

    public void Repare()
    {
        m_broken = false;
    }
}
