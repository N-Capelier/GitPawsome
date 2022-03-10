using UnityEngine;

public enum Archetype
{
	Support,
	Tank,
	Dps,
	Common
}

//[CreateAssetMenu(fileName = "New Spell", menuName = "Spell", order = 51)]
public class Spell : ScriptableObject
{
	public string spellName;
	public Sprite spellSprite;
	public Sprite spellRangeSprite;
	public Sprite notificationSprite;
	public string description;
	public GameObject particleEffect;
	[Space]
	public Archetype spellClass;
	public int attackDamages;
	public int healAmount;
	public int manaCost;
	[Space]
	public int productionPrice;
	public double productionTime;

	public virtual Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		return null;
	}

	public virtual void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		return;
	}
}
