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

public class PlayerManager : Singleton<PlayerManager>
{
    public string PlayerName = "Nyan Cat";
    public double CatFood;
    public double CatBell;
    public List<InstaCat> MyCats;
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
    public bool UseSpell(Spell TheSpell)
    {
        if(MyBagSpell.Count == 0)
        {
            return false;
        }
        for (int i = 0; i > MyBagSpell.Count; i++)
        {
            if (MyBagSpell[i].MySpell == TheSpell && MyBagSpell[i].InBag > 0)
            {
                MyBagSpell[i].InBag--;
                return true;
            }
        }
        return false;
    }
    public void AddCat(InstaCat Cat)
    {
        MyCats.Add(Cat);
    }
    

}
