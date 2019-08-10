using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbManager : Singleton<LightBulbManager>
{
    public LightHubController m_hub = null;
    public GameObject m_tutorial = null;
    public GameObject m_startSafe = null;
    // Start is called before the first frame update
    void Awake()
    {
        //ReleaseBulb();
        m_hub.Take();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasBulb => m_hub == null;

    public void TakeBulb(LightHubController hub)
    {
        m_hub = hub;
        CanvasController.Instance.MakeAvailable(false);
    }
    public void ReleaseBulb()
    {
            m_tutorial.SetActive(false);
        m_startSafe.SetActive(false);
        m_hub = null;
        CanvasController.Instance.MakeAvailable(true);
    }
}
