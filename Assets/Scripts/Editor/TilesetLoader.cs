using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[CustomEditor(typeof(Map))]
public class TilesetLoader : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Map myTarget = (Map)target;

		if (GUILayout.Button("Load Spritesheet"))
		{
			foreach (Object sprite in AssetDatabase.LoadAllAssetsAtPath(myTarget.GetSpritesheetPath()))
			{
				if (sprite.GetType() == typeof(Sprite))
				{
					myTarget.AddSprite((Sprite)sprite);
				}
			}
		}
	}
}
