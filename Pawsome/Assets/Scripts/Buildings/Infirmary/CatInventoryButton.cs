using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
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
