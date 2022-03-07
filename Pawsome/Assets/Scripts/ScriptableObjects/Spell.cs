using UnityEngine;

public enum AttackPattern
{
	Line,
	Diamond,
	Circle,
	Square,
}

public enum Archetype
{
	Support,
	Tank,
	Dps,
	Common
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell", order = 51)]
public class Spell : ScriptableObject
{
	public string spellName;
	public Sprite spellSprite;
	public string description;
	[Space]
	public Archetype spellClass;
	public AttackPattern attackPattern;
	public int attackDamages;
	public int manaCost;
	public int attackRange;
	[Space]
	public int productionPrice;
	public double productionTime;
}
