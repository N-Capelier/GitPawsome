using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
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
