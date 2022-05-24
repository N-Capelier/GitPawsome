using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Created by Kévin
/// Last modified by Kévin
/// </summary>
public class GameManager : Singleton<GameManager>
{

    public TextMeshProUGUI UIName;
    public TextMeshProUGUI UICatFood;
    public TextMeshProUGUI UICatBell;

    // Start is called before the first frame update
    void Start()
    {
        UIName.text = PlayerManager.Instance.PlayerName;
        BuildingManager.Instance.UpdateCatFood();
        BuildingManager.Instance.UpdateInvocation();
        BuildingManager.Instance.UpdateSpellGenerator();
        BuildingManager.Instance.UpdateCatDeckBuilding();
        BuildingManager.Instance.UpdateSpellDeckBuilding();
        BuildingManager.Instance.UpdateCatDeck(true);
        BuildingManager.Instance.UpdateInfirmaryInventory();
        BuildingManager.Instance.UpdateInfirmary(false);

    }

    public void UpdateUI()
    {
        UICatFood.text = PlayerManager.Instance.CatFood.ToString();
        UICatBell.text = PlayerManager.Instance.CatBell.ToString();
    }
    
    public void EndBattle()
    {
        for (int i = 0; i < PlayerManager.Instance.MyCatBag.Count; i++)
        {
            PlayerManager.Instance.MyCatBag[i].MyCat.spells.Clear();
        }
    }

}
