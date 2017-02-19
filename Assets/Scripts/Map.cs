using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	private const long MAP_WIDTH = int.MaxValue;

	[SerializeField]
	private GameObject prefabTile;
	[SerializeField]
	private List<Sprite> sprites;
	[SerializeField]
	private string spritesheetPath;

	private int activeSpriteIndex;

	private Dictionary<long, GameObject> tiles;

	private void Start()
	{
		tiles = new Dictionary<long, GameObject>();
	}

	private void Update()
	{

		if (Input.GetMouseButtonDown(InputManager.BUTTON_MOUSE_LEFT) && !FindUIController().IsUIOpen())
		{
			Vector2 mousePos = Input.mousePosition;
			mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
			mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
			Vector3 mouseInWorld = FindCamera().ScreenToWorldPoint(mousePos);
			GameObject tileObj = GetTile(Mathf.RoundToInt(mouseInWorld.x), Mathf.RoundToInt(mouseInWorld.y));
			tileObj.GetComponent<Tile>().SetSpriteIndex(activeSpriteIndex);
		}
	}

	public void SetActiveSpriteIndex(int activeSpriteIndex)
	{
		this.activeSpriteIndex = activeSpriteIndex;
	}

	public Sprite[] GetAllSprites()
	{
		return sprites.ToArray();
	}

	public Sprite GetSprite(int spriteIndex)
	{
		return sprites[spriteIndex];
	}

	public void AddSprite(Sprite sprite)
	{
		sprites.Add(sprite);
	}

	public string GetSpritesheetPath()
	{
		return spritesheetPath;
	}

	private GameObject MakeNewTile(int x, int y)
	{
		GameObject tile = Instantiate(prefabTile);
		tile.transform.position = new Vector2(x, y);
		tile.transform.parent = transform;
		return tile;
	}

	private GameObject GetTile(int x, int y)
	{
		long index = GetIndex(x, y);
		if (tiles.ContainsKey(index))
		{
			return tiles[index];
		}
		else
		{
			GameObject tile = MakeNewTile(x, y);
			SetTile(x, y, tile);
			return tile;
		}
	}

	private void SetTile(int x, int y, GameObject tile)
	{
		long index = GetIndex(x, y);
		if (tiles.ContainsKey(index))
		{
			tiles[index] = tile;
		}
		else
		{
			tiles.Add(index, tile);
		}
	}

	private long GetIndex(int x, int y)
	{
		return y * MAP_WIDTH + x;
	}

	private static Camera FindCamera()
	{
		return FindObjectOfType<Camera>();
	}

	private static UIController FindUIController()
	{
		return FindObjectOfType<UIController>();
	}
}
