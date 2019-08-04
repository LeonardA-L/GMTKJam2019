using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : Singleton<DeathManager>
{
    public bool IsDeath = false;
    public Transform m_checkpoint = null;
    public float m_gauge = 0;
    public float m_gaugeSpeed = 0.1f;
    public bool IsSafe = true;

    // Start is called before the first frame update
    void Awake()
    {
        m_gauge = 0;
        IsSafe = false; // TODO
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDeath || InteractibleUIController.Instance.IsMenu){
            return;
        }
        if (!LightHubController.m_lit && !IsSafe)
        {
            Debug.Log("Spoooooky");
        }
    }

    private float Distance(Transform checkpoint)
    {
        return (checkpoint.position - Character.Instance.gameObject.transform.position).magnitude;
    }

    public void Die()
    {
        IsDeath = true;
        MenuController.Instance.OpenDeathMenu();
    }

    public void Resurect()
    {
        Character.Instance.gameObject.transform.position = m_checkpoint.position;
        LightBulbManager.Instance.m_hub?.Release();
        SwitchboardController.Instance.Toggle();
        MenuController.Instance.CloseDeathMenu();
        m_gauge = 0;
        IsDeath = false;
    }
}
