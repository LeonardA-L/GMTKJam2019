using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbManager : Singleton<LightBulbManager>
{
    public bool m_hasBulb = true;

    // Start is called before the first frame update
    void Awake()
    {
        ReleaseBulb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasBulb => m_hasBulb;

    public void TakeBulb()
    {
        m_hasBulb = false;
        CanvasController.Instance.MakeAvailable(false);
    }
    public void ReleaseBulb()
    {
        m_hasBulb = true;
        CanvasController.Instance.MakeAvailable(true);
    }
}
