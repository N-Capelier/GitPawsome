using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
[System.Serializable]
public struct BorderColors
{
    public Color allyColor;
    public Color enemyColor;
    public Color currentAllyColor;
    public Color currentEnemyColor;
}

[System.Serializable]
public struct CardHightlightColors
{
    public Color baseColor;
    public Color hoverColor;
    public Color selectedColor;
}

public enum AllStats
{
    Attack,
    Defense,
    Pow,
    Move,
}

[System.Serializable]
public class ClassIconDictionary : SerializableDictionary<Archetype, Sprite> { }
[System.Serializable]
public class StatsIconDictionary : SerializableDictionary<AllStats, Sprite> { }


[System.Serializable]
[CreateAssetMenu(fileName = "NewGraphicReferences", menuName = "GraphicRessources", order = 50)]
public class GraphicReferences : ScriptableObject
{
    public BorderColors entityColorCode;
    public ClassIconDictionary classIcons;
    public ClassIconDictionary cardBackground;
    public StatsIconDictionary statsIcon;
    public CardHightlightColors cardHighlight;
}
