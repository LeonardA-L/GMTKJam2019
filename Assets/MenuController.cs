using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MenuController : Singleton<MenuController>
{
    public float m_transTime = 0.8f;
    public float m_deathTransTime = 1.5f;
    public PostProcessVolume m_postproc = null;
    public GameObject m_menuWrapper = null;
    public GameObject m_deathmenuWrapper = null;
    public Image m_deathmenuBg = null;
    public GameObject m_igUIWrapper = null;
    public Button m_backbutton = null;
    public Button m_deathbackbutton = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_menuWrapper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
           || Input.GetKeyDown(KeyCode.P))
        {
            if (InteractibleUIController.Instance.IsMenu)
            {
                StartCloseMenu();
            } else
            {
                StartCoroutine(OpenMenu());
            }
        }
    }

    public void StartCloseMenu()
    {
        StartCoroutine(CloseMenu());
    }

    private IEnumerator OpenMenu()
    {
        m_igUIWrapper.SetActive(false);
        InteractibleUIController.Instance.OpenMenu();
        float t = m_transTime;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_postproc.weight = Mathf.Lerp(0, 1, (1-(t / m_transTime)));
            yield return null;
        }
        m_menuWrapper.SetActive(true);
        yield return null;
        m_backbutton.Select();
    }

    private IEnumerator CloseMenu()
    {
        float t = m_transTime;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_postproc.weight = Mathf.Lerp(1, 0, (1-(t / m_transTime)));
            yield return null;
        }
        InteractibleUIController.Instance.CloseMenu();
        m_igUIWrapper.SetActive(true);
        m_menuWrapper.SetActive(false);
    }

    public void OpenDeathMenu()
    {
        StartCoroutine(OpenDeathMenuCor());
    }

    private IEnumerator OpenDeathMenuCor()
    {
        m_igUIWrapper.SetActive(false);
        InteractibleUIController.Instance.OpenMenu();
        AudioManager.Instance.PlaySound("Death");
        m_deathmenuWrapper.SetActive(true);
        float t = m_deathTransTime;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_postproc.weight = Mathf.Lerp(0, 1, (1 - (t / m_deathTransTime)));
            m_deathmenuBg.color = Color.Lerp( new Color(0,0,0,0), new Color(0, 0, 0, 1), (1 - (t / m_deathTransTime)));
            yield return null;
        }
        yield return null;
        m_deathbackbutton.Select();
    }

    public void CloseDeathMenu()
    {
        StartCoroutine(CloseDeathMenuCor());
    }

    private IEnumerator CloseDeathMenuCor()
    {
        float t = m_deathTransTime;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_postproc.weight = Mathf.Lerp(1, 0, (1 - (t / m_deathTransTime)));
            m_deathmenuBg.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), (1 - (t / m_deathTransTime)));
            yield return null;
        }
        m_deathmenuWrapper.SetActive(false);
        InteractibleUIController.Instance.CloseMenu();
        m_igUIWrapper.SetActive(true);
    }
}
