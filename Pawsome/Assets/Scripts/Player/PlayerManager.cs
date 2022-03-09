using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBag
{
    public Spell MySpell;
    public int InBag;
    public SpellBag(Spell theSpell, int i)
    {
        this.MySpell = theSpell;
        this.InBag = i;
    }
}
public class CatBag
{
    public InstaCat MyCat;
    public bool InUse;
    public CatBag(InstaCat theCat, bool used)
    {
        this.MyCat = theCat;
        this.InUse = used;
    }
}

public class PlayerManager : Singleton<PlayerManager>
{
    public string PlayerName = "Nyan Cat";
    public double CatFood;
    public double CatBell;
    public List<CatBag> MyCatBag = new List<CatBag>();
    public List<SpellBag> MyBagSpell = new List<SpellBag>();

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void IncrementCatFood(double x)
    {
        CatFood += x;
        GameManager.Instance.UpdateUI();
    }
    public void IncrementCatBell(double x)
    {
        CatBell += x;
        GameManager.Instance.UpdateUI();
    }
    public void AddSpell(Spell TheSpell)
    {
        if(MyBagSpell.Count == 0)
        {
            MyBagSpell.Add(new SpellBag(TheSpell ,1));
            return;
        }
        foreach(SpellBag element in MyBagSpell)
        {
            if(element.MySpell == TheSpell)
            {
                element.InBag++;
                return;
            }
        }
        MyBagSpell.Add(new SpellBag(TheSpell, 1));
    }
    public void UseSpell(Spell TheSpell)
    {
        for (int i = 0; i > MyBagSpell.Count; i++)
        {
            if (MyBagSpell[i].MySpell == TheSpell && MyBagSpell[i].InBag > 0)
            {
                MyBagSpell[i].InBag--;
                return;
            }
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
