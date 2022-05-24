using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class GeneratorButton : MonoBehaviour
{
    
    public int id;
    
    public void OnClick()
    {
        BuildingManager.Instance.OutCatInCatGenerator(id);
    }

}
