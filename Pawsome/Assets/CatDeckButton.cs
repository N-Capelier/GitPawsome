using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDeckButton : MonoBehaviour
{
    public int id;
    public bool DontHaveCat;

    
    public void OnClick()
    {
        if(!DontHaveCat)
        {
            DontHaveCat = true;
            BuildingManager.Instance.UpdateCatDeck(id, true);
            BuildingManager.Instance.DontWannaUseCat(id);

        }
        
    }
    
    public void ReceieveCat(int i)
    {
        DontHaveCat = false;
        id = i;
        BuildingManager.Instance.UpdateCatDeck(i, false);
        BuildingManager.Instance.UpdateCatDeckBuilding();
    }

}
