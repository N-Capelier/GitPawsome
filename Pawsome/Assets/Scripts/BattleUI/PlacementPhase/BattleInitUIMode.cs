using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInitUIMode : MonoBehaviour
{
    public BattleStateMachine fsm;

    public UnitPlacementUI placementUI;
    public TossCoinUI coinUI;

    List<InstaCat> placedInstaCats = new List<InstaCat>();

    [Header("Ressources")]
    public GraphicReferences graphicRessources;

    [Header("Temp")]
    public List<InstaCat> temp;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        coinUI.gameObject.SetActive(true);
        coinUI.OnMount(this, fsm.coinFlipAnimationDuration);
    }

    public void InitPlacement()
    {
        coinUI.OnUnMount();
        coinUI.gameObject.SetActive(false);

        var allPlayerCat = BattleInformationManager.Instance.GetInstaCats();
        var catToDisplay = allPlayerCat.Where(c => !placedInstaCats.Contains(c));

        placementUI.gameObject.SetActive(true);
        placementUI.OnMount(catToDisplay.ToList(), this);
    }

    public void StartGridPlacement(InstaCat catToPlace)
	{
        placedInstaCats.Add(catToPlace);
        placementUI.OnUnMount();
        placementUI.gameObject.SetActive(false);
        fsm.PickArchetype?.Invoke(catToPlace.catClass);
	}

    public Sprite GetClassIcon(InstaCat cat)
    {
        var sprite = graphicRessources.classIcons
            .Where(i => i.Key == cat.catClass)
            .Select(i => i.Value)
            .First();

        return sprite;
    }

    public Sprite GetStatIcon(AllStats stat)
    {
        var sprite = graphicRessources.statsIcon
            .Where(i => i.Key == stat)
            .Select(i => i.Value)
            .First();

        return sprite;
    }

    public Sprite GetCardBackground(Archetype _class)
    {
        var sprite = graphicRessources.cardBackground
            .Where(i => i.Key == _class)
            .Select(i => i.Value)
            .First();

        return sprite;
    }

    public Color GetHighLightColor(UnitCard.State state)
    {
        switch (state)
        {
            case UnitCard.State.Base:
                return graphicRessources.cardHighlight.baseColor;
            case UnitCard.State.Hover:
                return graphicRessources.cardHighlight.hoverColor;
            case UnitCard.State.Selected:
                return graphicRessources.cardHighlight.selectedColor;
            default:
                return Color.white;
        }
    }
}
