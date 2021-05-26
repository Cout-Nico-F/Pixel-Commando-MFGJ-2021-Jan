using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerPosition : MonoBehaviour
{
    [SerializeField] Transform m_PlayerTransform;
    [SerializeField] Vector3 m_PlayerPosition;
    
    void Update()
    {
        m_PlayerPosition = m_PlayerTransform.position;
        transform.position = m_PlayerPosition;
    }
}
