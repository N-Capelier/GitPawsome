using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public string PlayerName = "Nyan Cat";
    public double CatFood;
    public double CatBell;
    public InstaCat[] MesChats;

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void IncrementCatFood(double x)
    {
        CatFood += x;
    }
    public void IncrementCatBell(double x)
    {
        CatBell += x;
    }
    

}
