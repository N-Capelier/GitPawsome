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
	public InstaCat CatsDeck;
	public GameObject NoSpellPrefab;
	public GameObject DeckSpellPrefab;
	public Sprite NoCatPrefab;
	//CatDeck
	public GameObject CatUI;
	public GameObject CatImage;
	public GameObject CatSpells;


	private void Awake()
	{
		CreateSingleton(true);
	}
	void Start()
	{
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
		if (!InvocationBuilding.TryBuy(1)) YouCantPopUp.SetActive(true);
	}
	public void Booster2()
	{
		if (!InvocationBuilding.TryBuy(2)) YouCantPopUp.SetActive(true);
	}
	public void Booster3()
	{
		if (!InvocationBuilding.TryBuy(3)) YouCantPopUp.SetActive(true);
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
		if (SpellManager.Instance.Spells.Count < 1)
		{
			return;
		}
		for (int i = 0; i < SpellManager.Instance.Spells.Count; i++)
		{
			GameObject _button = Instantiate(SpellPrefb, SpellCollection.transform);
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
		if (PlayerManager.Instance.MyCatBag.Count < 1)
		{
			return;
		}
		for (int i = CatBag.transform.childCount; i > 0; i--)
		{
			Destroy(CatBag.transform.GetChild(i - 1).gameObject);
		}
		for (int i = 0; i < PlayerManager.Instance.MyCatBag.Count; i++)
		{
			if (PlayerManager.Instance.MyCatBag[i].InUse == false)
			{
				GameObject _button = Instantiate(CatCardPrefb, CatBag.transform);
				_button.GetComponent<Image>().sprite = PlayerManager.Instance.MyCatBag[i].MyCat.CatSprite;
				_button.GetComponent<CatInventoryButton>().id = i;
			}
		}
	}
	public void UpdateSpellDeckBuilding()
	{
		if (PlayerManager.Instance.MyBagSpell.Count < 1)
		{
			return;
		}
		for (int i = spellBag.transform.childCount; i > 0; i--)
		{
			Destroy(spellBag.transform.GetChild(i - 1).gameObject);
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



	

	//CatPart
	#region
	public void CatUse(int i)
	{
		PlayerManager.Instance.UseCat(i, true);
		CatImage.GetComponent<CatDeckButton>().OnClick();
		CatsDeck = PlayerManager.Instance.MyCatBag[i].MyCat;
		CatImage.GetComponent<CatDeckButton>().ReceieveCat(i);

		UpdateCatDeckBuilding();
	}
	public void DontWannaUseCat(int i)
	{
		PlayerManager.Instance.UseCat(i, false);

		UpdateCatDeckBuilding();
	}
	public void UpdateCatDeck(bool NoCat)
	{
		for (int i = CatSpells.transform.childCount; i > 0; i--)
		{
			Destroy(CatSpells.transform.GetChild(i - 1).gameObject);
		}

		if (NoCat)
		{
			CatImage.GetComponent<Image>().sprite = NoCatPrefab;
			for (int i = 0; i < 8; i++)
			{
				GameObject _button = Instantiate(NoSpellPrefab, CatSpells.transform);
			}

			return;
		}
		CatImage.GetComponent<Image>().sprite = CatsDeck.CatSprite;
		UpdateSpellDeck(false);
	}
	#endregion

	//SpellPart
	#region
	public void UpdateSpellDeck(bool JustReset)
	{
		for (int i = CatSpells.transform.childCount; i > 0; i--)
		{
			Destroy(CatSpells.transform.GetChild(i - 1).gameObject);
		}

		if (!JustReset && CatsDeck.spells.Count != 0)
		{
			for (int i = 0; i < CatsDeck.spells.Count; i++)
			{
				GameObject _button = Instantiate(DeckSpellPrefab, CatSpells.transform);

				_button.GetComponent<Image>().sprite = CatsDeck.spells[i].spellSprite;

				_button.GetComponent<SpellDeckButton>().idInBag = CatsDeck.Temp[i];
			}

			for (int i = CatsDeck.spells.Count; i < CatsDeck.deckSize; i++)
			{
				GameObject _button = Instantiate(NoSpellPrefab, CatSpells.transform);
			}
			return;
		}

		for (int i = 0; i < 8; i++)
		{
			GameObject _button = Instantiate(NoSpellPrefab, CatSpells.transform);
		}
	}
	public void SpellUse(int i)
	{
		if (!CatImage.GetComponent<CatDeckButton>().DontHaveCat)
		{
			if (CatsDeck.spells.Count < CatsDeck.deckSize)
			{
				bool isOk = false;
				if(CatsDeck.catClass == PlayerManager.Instance.MyBagSpell[i].MySpell.spellClass)
                {
					int spellClassCount = 0;
					foreach (Spell element in CatsDeck.spells)
                    {
						if (CatsDeck.catClass == element.spellClass) spellClassCount++;
                    }
					isOk = spellClassCount < 6;

				}
				else if (Archetype.Common == PlayerManager.Instance.MyBagSpell[i].MySpell.spellClass)
				{
					int spellClassCount = 0;
					foreach (Spell element in CatsDeck.spells)
                    {
						if (Archetype.Common == element.spellClass) spellClassCount++;
                    }
					isOk = spellClassCount < 2;

				}
				if (isOk)
				{
					CatsDeck.spells.Add(PlayerManager.Instance.MyBagSpell[i].MySpell);
					PlayerManager.Instance.MyBagSpell[i].InBag--;
					CatsDeck.Temp.Add(i);
					UpdateSpellDeckBuilding();
					UpdateSpellDeck(false);
				}

			}
		}
	}
	public void DontWannaUseSpell(int InBag)
	{
		PlayerManager.Instance.AddSpell(PlayerManager.Instance.MyBagSpell[InBag].MySpell);
		CatsDeck.Temp.Remove(InBag);
		CatsDeck.spells.Remove(PlayerManager.Instance.MyBagSpell[InBag].MySpell);
		UpdateSpellDeckBuilding();

		UpdateSpellDeck(false);
	}
	#endregion
	#endregion


	#endregion


}
