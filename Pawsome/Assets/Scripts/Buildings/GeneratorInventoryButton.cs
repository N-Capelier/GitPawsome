using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CatFoodInventoryButton : MonoBehaviour
{
    
    public int id;
    
    public void OnClick()
    {
        BuildingManager.Instance.AddCatInCatFood(id);
    }

}
