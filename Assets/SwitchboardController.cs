using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchboardController : MonoBehaviour
{
    public bool m_available = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_available && Input.GetKeyDown(KeyCode.Space))
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
        UpdateTooltips();
    }
}
