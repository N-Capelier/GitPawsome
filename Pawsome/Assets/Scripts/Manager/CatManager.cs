using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
