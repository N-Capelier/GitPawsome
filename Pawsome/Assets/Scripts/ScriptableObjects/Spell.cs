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
	Suppotr,
	tank,
	Dps,
	Commun
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell", order = 51)]
public class Spell : ScriptableObject
{
	public string SpellName;
	[Space]
	public int ManaUsed;
	[Space]
	public int BaseAttack;
	[Space]
	public AttackType RangeType;
	[Space]
	public int Range;
	[Space]
	public string Description;
	[Space]
	public Class SpellClass;
}
