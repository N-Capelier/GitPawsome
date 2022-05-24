using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class SpellManager : Singleton<SpellManager>
{
    public List<Spell> Spells;


    private void Awake()
    {
        CreateSingleton(true);
    }
}
