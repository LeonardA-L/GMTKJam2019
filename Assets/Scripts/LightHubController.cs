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

    public Vector2 m_timeConstraints;
    public float m_life = 0;

    // Start is called before the first frame update
    void Awake()
    {
        m_available = false;
        m_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_available && Input.GetKeyDown(KeyCode.Space) && !InteractibleUIController.Instance.IsInteracting)
        {
            Toggle();
        }
    }

    private void LateUpdate()
    {
        if (Character.Instance.HasMoved && m_on)
        {
            m_life -= Time.deltaTime;
            if(m_life <= 0)
            {
                Break();
            }
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
            TurnOff();
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
        m_life = UnityEngine.Random.Range(m_timeConstraints.x, m_timeConstraints.y);
    }

    private void TurnOff()
    {
        m_on = false;
        m_light.gameObject.SetActive(m_on);
    }

    public void Repare()
    {
        m_broken = false;
        if (m_hasBulb)
        {
            TurnOn();
        }
    }

    private void Break()
    {
        m_broken = true;
        TurnOff();
        SwitchboardController.Instance.ShowDamage();
    }

    public bool IsOn => m_on;
}
