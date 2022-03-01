using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInteractor : MonoBehaviour
{
	[Header("References")]
	[SerializeField] MeshRenderer meshRenderer;

	Material material;

	private void Start()
	{
		material = meshRenderer.material;
	}

	public void SetRendererVisibility(bool _isVisible)
	{
		if (_isVisible) material.SetInt("isVisible", 1);
		else material.SetInt("isVisible", 0);
	}

	public void SetRendererColor(Color _color)
	{
		material.SetColor("BaseColor", _color);
	}
}