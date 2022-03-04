using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("SpellGenerator")]
    public GameObject SpellGeneratorMenu;
    public SpellGenerator SpellBuilding;
    public GameObject SpellCollection;
    public GameObject SpellPrefb;
    
    [Header("DeckBuilding")]
    public GameObject DeckBuildingMenu;
    public GameObject CatPartMenu;
    public GameObject SpellPartMenu;
    public SpellGenerator DeckBuildingBuilding;
    public GameObject spellBag;
    public GameObject SpellCardPrefb;
    public GameObject CatBag;
    public GameObject CatCardPrefb;


    private void Awake()
    {
        CreateSingleton(true);
    }

    //CatFood
    #region 
    public void OpenCatFoodUI()
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
        UILevel.text = "Level " + CatFoodBuilding.level.ToString();
        UICreationTime.text = "Creation Time : " + CatFoodBuilding.CreationTime.ToString() + "s";
        UIProduction.text = "Production :" + CatFoodBuilding.FoodsPerCollection.ToString() + " Foods";
        UINextCreationTime.text = "Creation Time : " + (CatFoodBuilding.CreationTime - CatFoodBuilding.ReducedTime).ToString() + "s";
        UINextProduction.text = "Production :" + ((int)(CatFoodBuilding.FoodsPerCollection * CatFoodBuilding.CatFoodMultiplier)).ToString() + " Foods";
        UIPrice.text = CatFoodBuilding.LevelUpPrice.ToString() + "CatFood";
    }
    #endregion

    //Invocation
    #region

    public void OpenInvocationUI()
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

    //SpellGenerator
    #region

    public void OpenSpellGeneratorUI()
    {
        SpellGeneratorMenu.SetActive(true);
    }
    public void ExitSpellGenerator()
    {
        SpellGeneratorMenu.SetActive(false);
    }
    public void UpdateSpellGenerator()
    {
        if(SpellManager.Instance.Spells.Count < 1)
        {
            return;
        }
        for(int i = 0; i < SpellManager.Instance.Spells.Count; i++)
        {
            GameObject _button =  Instantiate(SpellPrefb, SpellCollection.transform);
            _button.GetComponent<Image>().sprite = SpellManager.Instance.Spells[i].SpellSprite;
            _button.GetComponent<SpellButton>().id = i;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = SpellManager.Instance.Spells[i].ProductionPrice.ToString();
        }
    }

    public bool TryProduce(int i)
    {
        return SpellBuilding.TryProduce(SpellManager.Instance.Spells[i].ProductionPrice);
    }
    public double GetProductionTime(int i)
    {
        return SpellManager.Instance.Spells[i].ProductionTime;
    }

    public void ProductionDone(int i)
    {
        SpellBuilding.GenerateSpell(i);
    }


    #endregion

    //DeckBuilding
    #region

    public void OpenDeckBuildingUI()
    {
        DeckBuildingMenu.SetActive(true);
    }
    public void ExitDeckBuilding()
    {
        DeckBuildingMenu.SetActive(false);
    }
    public void UpdateCatDeckBuilding()
    {  
        if(CatManager.Instance.Cats.Count < 1)
        {
            return;
        }
        for(int i = 0; i < CatManager.Instance.Cats.Count; i++)
        {
            GameObject _button =  Instantiate(CatCardPrefb, CatBag.transform);
            _button.GetComponent<Image>().sprite = CatManager.Instance.Cats[i].CatSprite;
        }
    }
    public void UpdateSpellDeckBuilding()
    {
        Debug.Log(PlayerManager.Instance.MyBagSpell.Count);
        if(PlayerManager.Instance.MyBagSpell.Count < 1)
        {
            return;
        }
        for (int i = spellBag.transform.childCount; i > 0; i--)
        {
            Destroy(spellBag.transform.GetChild(i-1).gameObject);
        }


        for (int i = 0; i < PlayerManager.Instance.MyBagSpell.Count; i++)
        {
            
            GameObject _button =  Instantiate(SpellCardPrefb, spellBag.transform);
            _button.GetComponent<Image>().sprite = PlayerManager.Instance.MyBagSpell[i].MySpell.SpellSprite;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "x" + PlayerManager.Instance.MyBagSpell[i].InBag.ToString();
        }
    }
    public void OpenCatPartUI()
    {
        CatPartMenu.SetActive(true);
        SpellPartMenu.SetActive(false);
    }
    public void OpenSpellPartUI()
    {
        CatPartMenu.SetActive(false);
        SpellPartMenu.SetActive(true);
    }

    #endregion

}
