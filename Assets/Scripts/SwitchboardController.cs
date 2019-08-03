using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchboardController : Singleton<SwitchboardController>
{
    public bool m_available = false;

    public GameObject m_ok = null;
    public GameObject m_nok = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_ok.SetActive(true);
        m_nok.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_available && Input.GetKeyDown(KeyCode.Space) && !InteractibleUIController.Instance.IsInteracting)
        {
            Toggle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            m_available = true;
            UpdateTooltips();
        }
    }

    private void UpdateTooltips()
    {
        CanvasController.Instance.RemoveTooltips();
        if (m_available)
        {
            CanvasController.Instance.TooltipFuses();
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
        var foundObjects = FindObjectsOfType<LightHubController>();
        foreach (var item in foundObjects)
        {
            item.Repare();
        }
        m_nok.SetActive(false);
        m_ok.SetActive(true);
        UpdateTooltips();
    }

    public void ShowDamage()
    {
        m_nok.SetActive(true);
        m_ok.SetActive(false);
    }
}
