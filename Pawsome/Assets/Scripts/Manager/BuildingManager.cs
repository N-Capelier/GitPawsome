using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingManager : Singleton<BuildingManager>
{
    [Header("CatFood")]
    public GameObject CatFoodMenu;
    public CatFood CatFoodBuilding;
    public TextMeshProUGUI UILevel;
    public TextMeshProUGUI UICreationTime;
    public TextMeshProUGUI UIProduction;
    public TextMeshProUGUI UINextCreationTime;
    public TextMeshProUGUI UINextProduction;
    public TextMeshProUGUI UIPrice;
    
    [Header("Invocation")]
    public GameObject InvocationMenu;
    public GameObject YouCantPopUp;
    public Invocation InvocationBuilding;
    public TextMeshProUGUI UIFirstPrice;
    public TextMeshProUGUI UISecondPrice;
    public TextMeshProUGUI UIThirdPrice;


    //CatFood
    #region 
    public void CatFoodUI()
    {
        CatFoodMenu.SetActive(true);
    }
    public void ExitCatFood()
    {
        CatFoodMenu.SetActive(false);
    }
    public void UpgradeCatFood()
    {
        if (CatFoodBuilding.TryUpgrade())
        {
            CatFoodBuilding.Upgraded();
            UpdateCatFood();
        }
    }
    public void UpdateCatFood()
    {
        GameManager.Instance.UpdateUI();
        UILevel.text = "Level " + CatFoodBuilding.GetLevel().ToString();
        UICreationTime.text = "Creation Time : " + CatFoodBuilding.CreationTime.ToString() + "s";
        UIProduction.text = "Production :" + CatFoodBuilding.FoodsPerCollection.ToString() + " Foods";
        UINextCreationTime.text = "Creation Time : " + (CatFoodBuilding.CreationTime - CatFoodBuilding.ReducedTime).ToString() + "s";
        UINextProduction.text = "Production :" + ((int)(CatFoodBuilding.FoodsPerCollection * CatFoodBuilding.CatFoodMultiplier)).ToString() + " Foods";
        UIPrice.text = CatFoodBuilding.LevelUpPrice.ToString() + "CatFood";
    }
    #endregion

    #region

    public void InvocationUI()
    {
        InvocationMenu.SetActive(true);
    }
    public void ExitInvocation()
    {
        InvocationMenu.SetActive(false);
    }
    public void UpdateInvocation()
    {
        UIFirstPrice.text = InvocationBuilding.FirstPrice.ToString();
        UISecondPrice.text = InvocationBuilding.SecondPrice.ToString();
        UIThirdPrice.text = InvocationBuilding.ThirdPrice.ToString();
    }

    public void Booster1()
    {
        Debug.Log("help me");
        if(!InvocationBuilding.TryBuy(1)) YouCantPopUp.SetActive(true);
    }
    public void Booster2()
    {
        if(!InvocationBuilding.TryBuy(2)) YouCantPopUp.SetActive(true);
    }
    public void Booster3()
    {
        if(!InvocationBuilding.TryBuy(3)) YouCantPopUp.SetActive(true);
    }
    public void ExitYouCantPopUp()
    {
        YouCantPopUp.SetActive(false);
    }

    #endregion


}
