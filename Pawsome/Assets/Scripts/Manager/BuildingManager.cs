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
    public int WichCat = 1;
    public List<InstaCat> CatsDeck;
    public GameObject NoSpellPrefab;
    public GameObject DeckSpellPrefab;
    public Sprite NoCatPrefab;
    public List<List<int>> TempList = new List<List<int>>();
    //CatDeck
    public List<GameObject> CatUI;
    public List<GameObject> CatImage;
    public List<GameObject> CatSpells;


    private void Awake()
    {
        CreateSingleton(true);
    }
    void Start()
    {
        TempList.Add(new List<int>());
        TempList.Add(new List<int>());
        TempList.Add(new List<int>());
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
            _button.GetComponent<Image>().sprite = SpellManager.Instance.Spells[i].spellSprite;
            _button.GetComponent<SpellButton>().id = i;
            _button.GetComponentInChildren<TextMeshProUGUI>().text = SpellManager.Instance.Spells[i].productionPrice.ToString();
        }
    }

    public bool TryProduce(int i)
    {
        return SpellBuilding.TryProduce(SpellManager.Instance.Spells[i].productionPrice);
    }
    public double GetProductionTime(int i)
    {
        return SpellManager.Instance.Spells[i].productionTime;
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
        UpdateCatDeckBuilding();
    }
    public void ExitDeckBuilding()
    {
        DeckBuildingMenu.SetActive(false);
    }

    //Inventory Part
    #region
    public void UpdateCatDeckBuilding()
    {  
        if(PlayerManager.Instance.MyCatBag.Count < 1)
        {
            return;
        }
        for (int i = CatBag.transform.childCount; i > 0; i--)
        {
            Destroy(CatBag.transform.GetChild(i - 1).gameObject);
        }
        for (int i = 0; i < PlayerManager.Instance.MyCatBag.Count; i++)
        {
            if(PlayerManager.Instance.MyCatBag[i].InUse == false)
            {
                GameObject _button =  Instantiate(CatCardPrefb, CatBag.transform);
                _button.GetComponent<Image>().sprite = PlayerManager.Instance.MyCatBag[i].MyCat.CatSprite;
                _button.GetComponent<CatInventoryButton>().id = i;
            }
        }
    }
    public void UpdateSpellDeckBuilding()
    {
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
            if (PlayerManager.Instance.MyBagSpell[i].InBag > 0)
            {
                GameObject _button = Instantiate(SpellCardPrefb, spellBag.transform);
                _button.GetComponent<Image>().sprite = PlayerManager.Instance.MyBagSpell[i].MySpell.spellSprite;
                _button.GetComponentInChildren<TextMeshProUGUI>().text = "x" + PlayerManager.Instance.MyBagSpell[i].InBag.ToString();
                _button.GetComponent<SpellInventoryButton>().id = i;
                _button.GetComponent<SpellInventoryButton>().spellLink = PlayerManager.Instance.MyBagSpell[i].MySpell;
            }
        }
    }
    public void OpenCatPartUI()
    {
        UpdateCatDeckBuilding();
        CatPartMenu.SetActive(true);
        SpellPartMenu.SetActive(false);
    }
    public void OpenSpellPartUI()
    {
        CatPartMenu.SetActive(false);
        SpellPartMenu.SetActive(true);
    }
    #endregion
    //DeckPart
    #region

    public void UpdateCatDeck(int k, bool NoCat)
    {
        for (int i = CatSpells[k].transform.childCount; i > 0; i--)
        {
            Destroy(CatSpells[k].transform.GetChild(i - 1).gameObject);
        }

        if (CatsDeck.Count < WichCat || NoCat)
        {
            CatImage[k].GetComponent<Image>().sprite = NoCatPrefab;
            for (int i = 0; i < 8; i++)
            {
                GameObject _button = Instantiate(NoSpellPrefab, CatSpells[k].transform);
            }

            return;
        }
        CatImage[k].GetComponent<Image>().sprite = CatsDeck[k].CatSprite;
        UpdateSpellDeck(false);
    }
    public void UpdateSpellDeck(bool JustReset)
    {
        int k = CatImage[WichCat-1].GetComponent<CatDeckButton>().id;
        for (int i = CatSpells[k].transform.childCount; i > 0; i--)
        {
            Destroy(CatSpells[k].transform.GetChild(i - 1).gameObject);
        }
        if (!JustReset)
        {
            for (int i = 0; i < CatsDeck[k].spells.Count; i++)
            {
                GameObject _button = Instantiate(DeckSpellPrefab, CatSpells[k].transform);
                _button.GetComponent<Image>().sprite = CatsDeck[k].spells[i].spellSprite;
                _button.GetComponent<SpellDeckButton>().idInBag = TempList[WichCat - 1][i];

            }

            for (int i = CatsDeck[k].spells.Count; i < CatsDeck[k].deckSize; i++)
            {
                GameObject _button = Instantiate(NoSpellPrefab, CatSpells[k].transform);
            }
            return;
        }
        for (int i = 0; i < 8; i++)
        {
            GameObject _button = Instantiate(NoSpellPrefab, CatSpells[k].transform);
        }



    }
    public void OpenCat1UI()
    {
        WichCat = 1;
        CatUI[0].SetActive(true);
        CatUI[1].SetActive(false);
        CatUI[2].SetActive(false);
    }
    public void OpenCat2UI()
    {
        WichCat = 2;
        CatUI[0].SetActive(false);
        CatUI[1].SetActive(true);
        CatUI[2].SetActive(false);
    }
    public void OpenCat3UI()
    {
        WichCat = 3;
        CatUI[0].SetActive(false);
        CatUI[1].SetActive(false);
        CatUI[2].SetActive(true);
        
    }

    //CatPart
    #region
    public void CatUse(int i)
    {
        PlayerManager.Instance.UseCat(i, true);
        if (CatsDeck.Count >= WichCat)
        {
            CatImage[WichCat - 1].GetComponent<CatDeckButton>().OnClick();
            CatsDeck[WichCat -1] = PlayerManager.Instance.MyCatBag[i].MyCat;
        }
        CatsDeck.Add(PlayerManager.Instance.MyCatBag[i].MyCat);
        CatImage[WichCat - 1].GetComponent<CatDeckButton>().ReceieveCat(i);

        UpdateCatDeckBuilding();
        UpdateCatDeck(i, false);
    }
    public void DontWannaUseCat(int i)
    {
        PlayerManager.Instance.UseCat(i, false);

        UpdateCatDeckBuilding();
    }
    #endregion

    //SpellPart
    #region
    public void SpellUse(int i)
    {
        if (!CatImage[WichCat - 1].GetComponent<CatDeckButton>().DontHaveCat)
        {
            if (CatsDeck[WichCat-1].spells.Count < CatsDeck[WichCat-1].deckSize)
            {
                CatsDeck[WichCat-1].spells.Add(PlayerManager.Instance.MyBagSpell[i].MySpell);
                PlayerManager.Instance.MyBagSpell[i].InBag --;
                TempList[WichCat - 1].Add(i);
                //Gerer l'initialisation de variables de "SpellDeckButton" dans update UI

            }
            UpdateSpellDeckBuilding();
            UpdateSpellDeck(false);
        }
    }
    public void DontWannaUseSpell(int i, int InBag)
    {
        Debug.Log("i = " + i);
        Debug.Log("in bag = " + InBag);
        PlayerManager.Instance.AddSpell(PlayerManager.Instance.MyBagSpell[InBag].MySpell);
        TempList[WichCat - 1].Remove(InBag);
        CatsDeck[WichCat - 1].spells.Remove(PlayerManager.Instance.MyBagSpell[i].MySpell);
        UpdateSpellDeckBuilding();
        Debug.Log("Spells count = " + CatsDeck[WichCat - 1].spells.Count);
        UpdateSpellDeck(CatsDeck[WichCat-1].spells.Count == 0);
    }
    #endregion
    #endregion


    #endregion


}
