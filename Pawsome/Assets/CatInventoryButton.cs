using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInventoryButton : MonoBehaviour
{
    public int id;


    public void OnClick()
    {
        BuildingManager.Instance.CatUse(id);

    }
}
