using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    public List<Spell> Spells;


    private void Awake()
    {
        CreateSingleton(true);
    }
}
