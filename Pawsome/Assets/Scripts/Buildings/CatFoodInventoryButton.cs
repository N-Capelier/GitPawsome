using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GeneratorInventoryButton : MonoBehaviour
{
    
    public int id;
    
    public void OnClick()
    {
        BuildingManager.Instance.AddCatInGenerator(id);
    }

}
