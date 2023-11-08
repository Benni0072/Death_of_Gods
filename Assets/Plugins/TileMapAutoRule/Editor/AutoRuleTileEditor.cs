using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

// ----------------------------------------------------------------------------
// Author: Alexandre Brull
// https://brullalex.itch.io/
// Extended by: João Azuaga
// https://github.com/razveck/AutoRuleTile
// ----------------------------------------------------------------------------

[CustomEditor(typeof(TerrainAutoRuleTile))]
[CanEditMultipleObjects]
public class AutoRuleTileEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		TerrainAutoRuleTile autoRuleTile = (TerrainAutoRuleTile)target;
		if(GUILayout.Button("Build Rule Tile")) {
			if(autoRuleTile.TileMap == null || autoRuleTile.RuleTileTemplate == null)
				return;

			autoRuleTile.OverrideRuleTile();
		}
	}
}
