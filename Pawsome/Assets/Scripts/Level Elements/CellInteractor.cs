using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInteractor : MonoBehaviour
{
	public LevelCell levelCell;

	[Header("References")]
	[SerializeField] MeshRenderer meshRenderer;

	Material material;

	private void Start()
	{
		material = meshRenderer.material;
	}

	public void SetRendererVisible(bool _isVisible)
	{
		//if (_isVisible) material.SetInt("isVisible", 1);
		//else material.SetInt("isVisible", 0);

		//if(_isVisible) material.set
	}

	public void SetRendererColor(Color _color)
	{
		material.SetColor("BaseColor", _color);
	}

	public void SetRendererAlpha(float _opacity)
	{
		material.SetFloat("Alpha", _opacity);
	}
}