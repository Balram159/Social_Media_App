using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using UnityEngine.Events;

public class AppManager : MonoBehaviour
{
    [Header("Debug")]
    private bool m_Debug;

    [Header("Events")]
    public UnityEvent OnLoggedIn;

    private void Start()
    {
        UniRESTClient.debugMode = m_Debug;

        _ = UniRESTClient.Async.ApplicationLogin((result) =>
        {
            OnLoggedIn?.Invoke();
        });
    }    
}
