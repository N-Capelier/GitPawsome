using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class SpellInventoryButton : MonoBehaviour
{
    public int id;
    public Spell spellLink;


    public void OnClick()
    {
        BuildingManager.Instance.SpellUse(id);

    }
}
