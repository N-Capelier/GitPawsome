using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : Singleton<CatFood>
{
    public int CreationTime;
    public int FoodsPerCollection;
    public double LevelUpPrice;
    public bool CanCollect;
    private float LastTimeCollect;
    public float PriceMultiplier;
    public float CatFoodMultiplier;
    public int ReducedTime;
    public int level = 1;
   

    // Update is called once per frame
    void Update()
    {
        if(!CanCollect)
        {
            CanCollect = (Time.time - LastTimeCollect) > CreationTime;
            return;
        }
        CanCollect = false;
        LastTimeCollect = Time.time;
        PlayerManager.Instance.IncrementCatFood(FoodsPerCollection);
        GameManager.Instance.UpdateUI();
    }

    public bool TryUpgrad()
    {
        return PlayerManager.Instance.CatFood > LevelUpPrice;
    }

    public void Upgraded()
    {
        PlayerManager.Instance.IncrementCatFood(-LevelUpPrice);
        level++;
        LevelUpPrice = (int)(LevelUpPrice * PriceMultiplier);
        FoodsPerCollection = (int)(CatFoodMultiplier*FoodsPerCollection);
        CreationTime -= ReducedTime;
        GameManager.Instance.UpdateCatFood();
    }


}
