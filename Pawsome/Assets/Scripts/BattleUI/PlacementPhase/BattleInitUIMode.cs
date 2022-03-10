using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInitUIMode : MonoBehaviour
{
    public BattleStateMachine fsm;

    public UnitPlacementUI placementUI;
    public TossCoinUI coinUI;

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
        coinUI.OnMount(this);
    }

    public void InitPlacement()
    {
        coinUI.OnUnMount();
        coinUI.gameObject.SetActive(false);
        //TODO: implement real list of cat
        placementUI.OnMount(temp, this);
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
