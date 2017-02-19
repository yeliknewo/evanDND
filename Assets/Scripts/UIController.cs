using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private bool uiOpen;

	private GameObject ui;

	[SerializeField]
	private GameObject prefabUI;
	[SerializeField]
	private GameObject prefabButton;

	private Dictionary<long, bool> buttonSpaces;

	private float buttonWidth;
	private float buttonHeight;

	public bool IsUIOpen()
	{
		return uiOpen;
	}

	private void Start()
	{
		MakeUI();
		SetUIOpen(false);
	}

	private void MakeUI()
	{
		buttonSpaces = new Dictionary<long, bool>();
		Vector3[] corners = new Vector3[4];
		prefabButton.GetComponent<RectTransform>().GetWorldCorners(corners);

		buttonWidth = corners[2].x - corners[0].x;
		buttonHeight = corners[2].y - corners[0].y;

		ui = Instantiate(prefabUI);
		ui.name = "UI";

		Sprite[] sprites = FindMap().GetAllSprites();
		for (int i = 0; i < sprites.Length; i++)
		{
			Sprite sprite = sprites[i];
			GameObject button = Instantiate(prefabButton);
			button.transform.position = FindButtonSpace();
			button.transform.SetParent(ui.transform);
			button.GetComponent<Image>().sprite = sprite;
			int spriteIndex = i;
			button.GetComponent<Button>().onClick.AddListener(() => OnClick(spriteIndex));
		}
	}

	private void OnClick(int spriteIndex)
	{
		Debug.Log(spriteIndex);
		FindMap().SetActiveSpriteIndex(spriteIndex);
	}

	private Vector2 FindButtonSpace()
	{
		for (int i = 0; i < 100; i++)
		{
			long x = Random.Range(0, Mathf.RoundToInt(Screen.width / buttonWidth));
			long y = Random.Range(0, Mathf.RoundToInt(Screen.height / buttonHeight));

			long index = y * int.MaxValue + x;
			if (buttonSpaces.ContainsKey(index))
			{
				if (!buttonSpaces[index])
				{
					buttonSpaces[index] = true;
					return new Vector2(x * buttonWidth, y * buttonHeight);
				}
			}
			else
			{
				buttonSpaces.Add(index, true);
				return new Vector2(x * buttonWidth, y * buttonHeight);
			}
		}
		Debug.LogError("Using Random Select To Find Button Space is Dumb and Didn't work");
		return Vector2.zero;
	}

	private void SetUIOpen(bool open)
	{
		uiOpen = open;
		if (uiOpen)
		{
			ui.GetComponent<Canvas>().enabled = true;
		}
		else
		{
			ui.GetComponent<Canvas>().enabled = false;
		}
	}

	private void ToggleUI()
	{
		SetUIOpen(!IsUIOpen());
	}

	private void Update()
	{
		if (Input.GetKeyDown(InputManager.BUTTON_KEY_SPACE))
		{
			ToggleUI();
		}
	}

	private static Map FindMap()
	{
		return FindObjectOfType<Map>();
	}
}
