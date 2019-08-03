using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractibleUIController : Singleton<InteractibleUIController>
{
    public GameObject m_wrapper = null;
    public TextMeshProUGUI m_text = null;
    public Image m_img = null;
    public Image m_imgShadow = null;

    public bool IsInteracting { get; private set; } = false;

    // Start is called before the first frame update
    void Awake()
    {
        m_wrapper.SetActive(false);
        IsInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonDown(0)
            )
        {
            Disable();
        }
    }

    private void Disable()
    {
        m_wrapper.SetActive(false);
        IsInteracting = false;
    }

    internal void Interact(InteractibleItem m_item)
    {
        m_wrapper.SetActive(true);
        m_text.text = m_item.m_text;
        m_img.sprite = m_item.m_2DSprite;
        m_imgShadow.sprite = m_item.m_2DSprite;
        IsInteracting = true;
    }
}
