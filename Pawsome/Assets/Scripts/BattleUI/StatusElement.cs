using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Created by R�mi
/// Last modified by R�mi
/// </summary>
public class StatusElement : MonoBehaviour
{
    [SerializeField]
    Image icon;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount()
    {
    }

    public void OnUnMount()
    {
    }
}
