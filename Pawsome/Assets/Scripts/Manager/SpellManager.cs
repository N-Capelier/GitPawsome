using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class SpellManager : Singleton<SpellManager>
{
    public List<Spell> Spells;


    private void Awake()
    {
        CreateSingleton(true);
    }
}
