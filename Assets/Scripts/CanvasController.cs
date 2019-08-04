using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : Singleton<CanvasController>
{
    public GameObject m_available;
    public GameObject m_unavailable;

    public GameObject m_place;
    public GameObject m_remove;
    public GameObject m_broken;
    public GameObject m_fuses;
    public GameObject m_interact;

    public GameObject m_spacebar;

    private void Awake()
    {
        RemoveTooltips();
    }

    public void MakeAvailable(bool v)
    {
        m_available.SetActive(v);
        m_unavailable.SetActive(!v);
    }

    public void TooltipPlace()
    {
        m_place.SetActive(true);
        m_spacebar.SetActive(true);
    }
    public void TooltipRemove()
    {
        m_remove.SetActive(true);
        m_spacebar.SetActive(true);
    }
    public void TooltipBroken()
    {
        m_broken.SetActive(true);
    }
    public void TooltipFuses()
    {
        m_fuses.SetActive(true);
        m_spacebar.SetActive(true);
    }
    public void TooltipInteract()
    {
        m_interact.SetActive(true);
        m_spacebar.SetActive(true);
    }

    public void RemoveTooltips()
    {
        m_spacebar.SetActive(false);
        m_place.SetActive(false);
        m_remove.SetActive(false);
        m_broken.SetActive(false);
        m_fuses.SetActive(false);
        m_interact.SetActive(false);
    }
}
