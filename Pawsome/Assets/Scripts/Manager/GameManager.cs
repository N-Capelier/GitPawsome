using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        BuildingManager.Instance.UpdateCatDeck(0, true);
        BuildingManager.Instance.UpdateCatDeck(1, true);
        BuildingManager.Instance.UpdateCatDeck(2, true);
    }

    public void UpdateUI()
    {
        UICatFood.text = PlayerManager.Instance.CatFood.ToString();
        UICatBell.text = PlayerManager.Instance.CatBell.ToString();
    }
    


}
