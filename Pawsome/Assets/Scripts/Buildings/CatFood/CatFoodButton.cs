using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class CatFoodButton : MonoBehaviour
{
    
    public int id;
    
    public void OnClick()
    {
        BuildingManager.Instance.OutCatInCatFood(id);
    }

}
