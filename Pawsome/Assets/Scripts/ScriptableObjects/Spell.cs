using UnityEngine;

public enum AttackType
{
	straight,
	diamond,
	circle,
	square,
	diagonal
}

public enum Class
{
	Support,
	tank,
	Dps,
	Commun
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell", order = 51)]
public class Spell : ScriptableObject
{
	public string SpellName;
	[Space]
	public Sprite SpellSprite;
	[Space]
	public int ManaUsed;
	[Space]
	public int BaseAttack;
	[Space]
	public AttackType RangeType;
	[Space]
	public int ProductionPrice;
	public double ProductionTime;
	[Space]
	public int Range;
	[Space]
	public string Description;
	[Space]
	public Class SpellClass;
}
