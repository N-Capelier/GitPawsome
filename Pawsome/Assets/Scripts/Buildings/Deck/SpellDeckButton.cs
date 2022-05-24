using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class SpellDeckButton : MonoBehaviour
{
    public int idInBag;

    
    public void OnClick()

    {
        BuildingManager.Instance.DontWannaUseSpell(idInBag);
    }

}
