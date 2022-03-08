using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDeckButton : MonoBehaviour
{
    public int id;
    public int idInBag;
    public Spell TheSpellWow;

    
    public void OnClick()
    {
        Debug.Log(id);
        BuildingManager.Instance.DontWannaUseSpell(id);
        BuildingManager.Instance.UpdateCatDeck(id, false);
    }

}
