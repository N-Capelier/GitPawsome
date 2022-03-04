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
}
