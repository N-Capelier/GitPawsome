using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InfirmaryInventoryButton : MonoBehaviour
{
    
    public int id;
    public Slider HpBar;
    public TextMeshProUGUI Hp;
    
    public void OnClick()
    {
        BuildingManager.Instance.InfirmaryCatUse(id);
    }

}
