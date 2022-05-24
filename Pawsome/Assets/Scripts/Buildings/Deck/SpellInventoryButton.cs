using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Kévin
/// Last modified by Kévin
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
