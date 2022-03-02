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
    public int ReducedTime;
    private int level = 1;

    [Header("Cat")]
    public GameObject WorkingCat;
    public bool CatIn;
    public float CatBoost;


    // Update is called once per frame
    void Update()
    {
        if(!CanCollect)
        {
            if (WorkingCat != null || CatIn)
            {

                CanCollect = (Time.time - LastTimeCollect) > (CreationTime / CatBoost);
            }
            CanCollect = (Time.time - LastTimeCollect) > CreationTime;
            return;
        }
        CanCollect = false;
        LastTimeCollect = Time.time;
        PlayerManager.Instance.IncrementCatFood(FoodsPerCollection);
        GameManager.Instance.UpdateUI();
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
        CreationTime -= ReducedTime;
    }


}
