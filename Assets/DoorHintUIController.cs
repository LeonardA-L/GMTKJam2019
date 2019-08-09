using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorHintUIController : MonoBehaviour
{
    public float m_timer = 4f;
    public float m_fade = 0.5f;
    public TextMeshProUGUI m_text = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        StartCoroutine(Decrease());
    }

    public IEnumerator Decrease()
    {
        m_text.color = new Color(1, 1, 1, 0);
        while(InteractibleUIController.Instance.IsInteracting || InteractibleUIController.Instance.IsMenu)
        {
            yield return null;
        }
        m_text.color = new Color(1, 1, 1, 1);

        float t = m_timer;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        t = m_fade;
        while (t >= 0)
        {
            t -= Time.deltaTime;
            m_text.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1 - (t/m_fade));
            yield return null;
        }
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
