using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : MonoBehaviour
{
    [Header("Stats")]
    public int CreationTime;
    public int FoodsPerCollection;
    public double LevelUpPrice;
    private bool CanCollect;
    private float LastTimeCollect;
    public float PriceMultiplier;
    public float CatFoodMultiplier;
    public int level = 1;

    [Header("Cat")]
    public InstaCat WorkingCat;
    public bool CatIn = false;
    public float CatSpeedBoost;


    // Update is called once per frame
    void Update()
    {
        if(!CanCollect)
        {
            if (CatIn)
            {

                CanCollect = (Time.time - LastTimeCollect) > (CreationTime - CatSpeedBoost);
            }
            CanCollect = (Time.time - LastTimeCollect) > CreationTime;
            return;
        }
        CanCollect = false;
        LastTimeCollect = Time.time;
        PlayerManager.Instance.IncrementCatFood(FoodsPerCollection);
    }

    public bool TryUpgrade()
    {
        return PlayerManager.Instance.CatFood > LevelUpPrice;
    }
    public int GetLevel()
    {
        return level;
    }
    public void Upgraded()
    {
        PlayerManager.Instance.IncrementCatFood(-LevelUpPrice);
        level++;
        LevelUpPrice = (int)(LevelUpPrice * PriceMultiplier);
        FoodsPerCollection = (int)(CatFoodMultiplier*FoodsPerCollection);
    }


}
