using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header("References")]
	[SerializeField] MeshRenderer objectRenderer;

	InstaCat instaCat;
	public InstaCat InstaCat { get => instaCat; private set => instaCat = value; }

	InstaCat instaCatRef;
	public InstaCat InstaCatRef { get => instaCatRef; private set => instaCatRef = value; }

	public void Init(InstaCat _instaCat)
	{
		instaCat = Instantiate(_instaCat);
	}

	public bool TakeDamage(int _damages)
	{


		return false;
	}
}
