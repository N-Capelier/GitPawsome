using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class SpellGenerator : MonoBehaviour
{
    public List<InstaCat> FakeCatBag = new List<InstaCat>();
    public List<CatBag> MyCatBag = new List<CatBag>();
    public bool CatIn = false;

    private void Start()
    {
        foreach (InstaCat element in FakeCatBag)
        {
            AddCat(element);
        }
    }
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
        if(CatIn)
        {
            PlayerManager.Instance.AddSpell(SpellManager.Instance.Spells[SpellId]);
            BuildingManager.Instance.UpdateSpellDeckBuilding();
        }
    }

    public void AddCat(InstaCat newCat)
    {
        MyCatBag.Add(new CatBag(newCat, false));
    }

    public void UseCat(int CatIndice, bool WannaUse)
    {
        MyCatBag[CatIndice].InUse = WannaUse;
        return;

    }
}
