using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class CatDeckButton : MonoBehaviour
{
    public int id;
    public bool DontHaveCat = false;

    
    public void OnClick()
    {
        if(!DontHaveCat)
        {
            DontHaveCat = true;
            BuildingManager.Instance.DontWannaUseCat(id);
            BuildingManager.Instance.UpdateCatDeck(true);

        }
        
    }
    
    public void ReceieveCat(int i)
    {
        DontHaveCat = false;
        id = i;
        BuildingManager.Instance.UpdateCatDeck(false);
    }

}
