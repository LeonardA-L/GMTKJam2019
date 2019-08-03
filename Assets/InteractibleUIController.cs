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

    private bool m_internalInteracting = false;
    public bool IsInteracting { get; private set; } = false;
    public bool IsMenu = true;

    public GameObject m_menuBtn = null;
    public Image m_menuBg = null;
    public Image m_menuLightning = null;
    public Image m_menuTitle = null;
    public float m_timerMax = 1f;
    public float m_titleFadeMax = 0.4f;

    // Start is called before the first frame update
    void Awake()
    {
        m_wrapper.SetActive(false);
        IsInteracting = true;
        m_internalInteracting = true;
        IsMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMenu)
            return;

        if (!m_internalInteracting)
        {
            IsInteracting = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Escape)
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
        m_internalInteracting = false;
    }

    internal void Interact(InteractibleItem m_item)
    {
        m_wrapper.SetActive(true);
        m_text.text = m_item.m_text;
        m_img.sprite = m_item.m_2DSprite;
        m_imgShadow.sprite = m_item.m_2DSprite;
        IsInteracting = true;
        m_internalInteracting = true;
    }

    public IEnumerator HideMenuCor()
    {
        m_menuBtn.SetActive(false);

        float t = m_timerMax;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_menuBg.color = Color.Lerp(Color.white, new Color(0, 0, 0, 0), (1-(t / m_timerMax)));
            m_menuLightning.color = Color.Lerp(Color.white, new Color(0, 0, 0, 0), (1 - (t / m_timerMax)));
            yield return null;
        }
        t = m_titleFadeMax;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_menuTitle.color = Color.Lerp(Color.white, new Color(0, 0, 0, 0), (1 - (t / m_titleFadeMax)));
            yield return null;
        }

        IsInteracting = false;
        m_internalInteracting = false;
        IsMenu = false;
        m_menuBg.gameObject.SetActive(false);
    }

    public void HideMenu()
    {
        StartCoroutine(HideMenuCor());
    }
}
