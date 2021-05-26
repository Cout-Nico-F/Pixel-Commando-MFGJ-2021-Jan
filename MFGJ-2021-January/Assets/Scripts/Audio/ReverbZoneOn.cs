using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ReverbZoneOn : MonoBehaviour
{
    [SerializeField] AudioReverbZone m_ReverbZone;
    [SerializeField] ReverbStatus m_ReverbStatus = new ReverbStatus();
        

    private void Awake()
    {

        if (m_ReverbZone == null)
        {
            m_ReverbZone = FindObjectOfType<AudioReverbZone>();
                
        }
    }

    private void Start()
    {
        m_ReverbZone.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(m_ReverbStatus == ReverbStatus.Activate && !m_ReverbZone.isActiveAndEnabled)
            {
                m_ReverbZone.gameObject.SetActive(true);

            }
            else if (m_ReverbStatus == ReverbStatus.Deactivate && m_ReverbZone.isActiveAndEnabled)
            {
                m_ReverbZone.gameObject.SetActive(false);
            }
            
        }   
    }
    
}
public enum ReverbStatus
{
    Activate,
    Deactivate
}

