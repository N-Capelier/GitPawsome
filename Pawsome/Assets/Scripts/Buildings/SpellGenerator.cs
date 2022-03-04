using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGenerator : MonoBehaviour
{
    public bool TryProduce(int price)
    {
        if(price < PlayerManager.Instance.CatFood)
        {
            PlayerManager.Instance.IncrementCatFood(-price);
            return true;
        }
        return false;
    }
    public void GenerateSpell(int SpellId)
    {
        PlayerManager.Instance.AddSpell(SpellManager.Instance.Spells[SpellId]);
        BuildingManager.Instance.UpdateSpellDeckBuilding();
    }
}
