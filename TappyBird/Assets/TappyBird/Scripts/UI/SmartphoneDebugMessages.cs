using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmartphoneDebugMessages : MonoBehaviour
{
    public static TextMeshProUGUI debugMessages;
    public TextMeshProUGUI debugMessagesTMP;

    void Awake()
    {
#if !UNITY_ANDROID
    if(debugMessagesTMP.gameObject != null)
        Destroy(debugMessagesTMP.gameObject);
    Destroy(this);
#endif

        debugMessages = debugMessagesTMP;
    }

}