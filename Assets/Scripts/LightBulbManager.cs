using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbManager : Singleton<LightBulbManager>
{
    public LightHubController m_hub = null;

    // Start is called before the first frame update
    void Awake()
    {
        ReleaseBulb();
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
        m_hub = null;
        CanvasController.Instance.MakeAvailable(true);
    }
}
