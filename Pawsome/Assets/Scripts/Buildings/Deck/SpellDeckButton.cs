using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class SpellDeckButton : MonoBehaviour
{
    public int idInBag;

    
    public void OnClick()

    {
        BuildingManager.Instance.DontWannaUseSpell(idInBag);
    }

}
