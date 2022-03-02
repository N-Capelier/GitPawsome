using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    public TextMeshProUGUI UIName;
    public TextMeshProUGUI UICatFood;
    public TextMeshProUGUI UICatBell;

    [Header("CatFood")]
    public TextMeshProUGUI UILevel;
    public TextMeshProUGUI UICreationTime;
    public TextMeshProUGUI UIProduction;
    public TextMeshProUGUI UINextCreationTime;
    public TextMeshProUGUI UINextProduction;
    public TextMeshProUGUI UIPrice;

    // Start is called before the first frame update
    void Start()
    {
        UIName.text = PlayerManager.Instance.PlayerName;
        UpdateCatFood();
    }

    public void UpdateUI()
    {
        UICatFood.text = PlayerManager.Instance.CatFood.ToString();
        UICatBell.text = PlayerManager.Instance.CatBell.ToString();
    }
    public void UpdateCatFood()
    {
        UpdateUI();
        UILevel.text = "Level " + CatFood.Instance.level.ToString();
        UICreationTime.text = "Creation Time : " + CatFood.Instance.CreationTime.ToString() + "s";
        UIProduction.text = "Production :" + CatFood.Instance.FoodsPerCollection.ToString() + " Foods";
        UINextCreationTime.text = "Creation Time : " + (CatFood.Instance.CreationTime - CatFood.Instance.ReducedTime).ToString() + "s";
        UINextProduction.text = "Production :" + ((int)(CatFood.Instance.FoodsPerCollection * CatFood.Instance.CatFoodMultiplier)).ToString() + " Foods";
        UIPrice.text = CatFood.Instance.LevelUpPrice.ToString() + "CatFood";
    }


}
