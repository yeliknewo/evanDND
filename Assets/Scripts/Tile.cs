using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private int spriteIndex;

	public int GetSpriteIndex()
	{
		return spriteIndex;
	}

	public void SetSpriteIndex(int spriteIndex)
	{
		this.spriteIndex = spriteIndex;
		GetSpriteRenderer().sprite = FindMap().GetSprite(spriteIndex);
	}

	private SpriteRenderer GetSpriteRenderer()
	{
		return GetComponent<SpriteRenderer>();
	}

	private static Map FindMap()
	{
		return FindObjectOfType<Map>();
	}
}
