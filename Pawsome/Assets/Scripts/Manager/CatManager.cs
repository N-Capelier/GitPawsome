using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by K�vin
/// Last modified by K�vin
/// </summary>
public class CatManager : Singleton<CatManager>
{
    public List<InstaCat> Cats;


    private void Awake()
    {
        CreateSingleton(true);
    }
    private void Start()
    {
        if (Cats.Count < 1)
        {
            return;
        }
        for (int i = 0; i < Cats.Count; i++)
        {
            PlayerManager.Instance.AddCat(Cats[i]);
        }
    }
}
