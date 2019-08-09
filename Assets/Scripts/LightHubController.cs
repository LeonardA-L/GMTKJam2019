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
    public Animator m_anim = null;
    //public Light m_light = null;

    public Vector2 m_timeConstraints;
    public float m_life = 0;

    public static bool m_lit = false;

    // Start is called before the first frame update
    void Awake()
    {
        m_available = false;
        m_on = false;
        m_lit = false;
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
        if (Character.Instance.HasMoved && m_on && !InteractibleUIController.Instance.IsInteracting && !InteractibleUIController.Instance.IsMenu)
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
            Release();
        }
        else if (LightBulbManager.Instance.HasBulb && !m_broken)
        {
            Take();
        }
        UpdateTooltips();
    }

    public void Take()
    {
        m_hasBulb = true;
        LightBulbManager.Instance.TakeBulb(this);
        AudioManager.Instance?.PlaySound("LampOn");
        TurnOn();
    }

    public void Release()
    {
        LightBulbManager.Instance.ReleaseBulb();
        m_hasBulb = false;
        TurnOff();
    }

    private void TurnOn()
    {
        m_on = true;
        m_lit = true;
        //m_light.gameObject.SetActive(m_on);
        m_anim.SetBool("Turned", m_on);
        m_life = UnityEngine.Random.Range(m_timeConstraints.x, m_timeConstraints.y);
    }

    private void TurnOff()
    {
        m_on = false;
        m_lit = false;
        m_anim.SetBool("Turned", m_on);
        //m_light.gameObject.SetActive(m_on);
    }

    public void Repare()
    {
        m_broken = false;
        m_anim.SetBool("Broken", false);
        if (m_hasBulb)
        {
            TurnOn();
        }
    }

    private void Break()
    {
        m_broken = true;
        TurnOff();
        m_anim.SetBool("Broken", true);
        AudioManager.Instance.PlaySound("LampBroken");
        SwitchboardController.Instance.ShowDamage();
    }

    public bool IsOn => m_on;
}
