using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleController : MonoBehaviour
{
    public InteractibleItem m_item = null;
    public bool m_available = false;
    public bool m_lit = false;
    public GameObject m_interactable = null;
    public List<LightHubController> m_lights = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_interactable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_lit = false;
        foreach (var item in m_lights)
        {
            if (item.IsOn)
            {
                m_lit = true;
                break;
            }
        }
        m_interactable.SetActive(m_lit);
        if (m_lit)
        {
            if (m_available)
            {
                UpdateTooltips();
            }
            if (m_available && Input.GetKeyDown(KeyCode.Space))
            {
                Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            m_available = true;
        }
    }

    private void UpdateTooltips()
    {
        CanvasController.Instance.RemoveTooltips();
        CanvasController.Instance.TooltipInteract();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            m_available = false;
            CanvasController.Instance.RemoveTooltips();
        }
    }

    private void Interact()
    {
        InteractibleUIController.Instance.Interact(m_item);
    }
}
