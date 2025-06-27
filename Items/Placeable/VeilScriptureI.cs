﻿
using LunarVeilLegacy.Tiles;
using LunarVeilLegacy.Tiles.Abyss.Aurelus;
using LunarVeilLegacy.Tiles.Catacombs;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
	public class VeilScriptureI : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<VeilScripture>());
			Item.width = 14;
			Item.height = 28;
			Item.value = 150;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}