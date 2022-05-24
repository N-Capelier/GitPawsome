using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class CatInventoryButton : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI Name;

    
    public void OnClick()
    {
        BuildingManager.Instance.CatUse(id);

    }
}
