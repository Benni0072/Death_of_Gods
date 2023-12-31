﻿using UnityEditor;
using UnityEngine;
using System.Linq;

// ----------------------------------------------------------------------------
// Author: Alexandre Brull
// https://brullalex.itch.io/
// Extended by: João Azuaga
// https://github.com/razveck/AutoRuleTile
// ----------------------------------------------------------------------------

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Terrain Auto Rule Tile", menuName = "Tiles/Terrain Auto Rule Tile")]
public class TerrainAutoRuleTile : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    public Texture2D TileMap;
    [SerializeField]
    public RuleTile RuleTileTemplate;
    RuleTile RuleTileTemplate_Default;

    public void OverrideRuleTile()
    {
        // Make a copy of the Rule Tile Template from a new asset.
        RuleTile _new = CreateInstance<RuleTile>();
        EditorUtility.CopySerialized(RuleTileTemplate, _new);

        // Get all the sprites in the Texture2D file (TileMap)
        string spriteSheet = AssetDatabase.GetAssetPath(TileMap);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet)
            .OfType<Sprite>().ToArray();

        if (sprites.Length != RuleTileTemplate.m_TilingRules.Count)
        {
            Debug.LogWarning("The Tilemap doesn't have the same number of sprites than the Rule Tile template has rules.");
        }

        // Set all the sprites of the TileMap.
        for (int i = 0; i < RuleTileTemplate.m_TilingRules.Count; i++)
        {
            _new.m_TilingRules[i].m_Sprites[0] = sprites[i];
            _new.m_DefaultSprite = sprites[24];
        }

        // Replace this Asset with the new one.
        AssetDatabase.CreateAsset(_new, AssetDatabase.GetAssetPath(this));
        Selection.activeObject = _new;
    }

	public void OnBeforeSerialize() {
		if (RuleTileTemplate_Default != null)
            return;

        RuleTileTemplate_Default = Resources.Load("AutoRuleTile_default") as RuleTile;
        if (RuleTileTemplate_Default != null)
        {
            RuleTileTemplate = RuleTileTemplate_Default;
        }
	}

	public void OnAfterDeserialize() {
        
	}
}