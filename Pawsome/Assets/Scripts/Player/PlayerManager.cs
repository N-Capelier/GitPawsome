using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public string PlayerName = "Nyan Cat";
    public double CatFood;
    public double CatBell;

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void IncrementCatFood(int x)
    {
        CatFood += x;
    }
    public void IncrementCatBell(int x)
    {
        CatBell += x;
    }
    

}
