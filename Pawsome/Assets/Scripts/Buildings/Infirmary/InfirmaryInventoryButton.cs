using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class InfirmaryInventoryButton : MonoBehaviour
{
    
    public int id;
    public Slider HpBar;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Price;
    
    public void OnClick()
    {
        BuildingManager.Instance.InfirmaryCatUse(id);
    }

}
