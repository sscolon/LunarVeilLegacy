﻿

using LunarVeilLegacy.Tiles.Catacombs;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
	public class CatacombsDoor : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<CatacombsDoorClosed>());
			Item.width = 14;
			Item.height = 28;
			Item.value = 150;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}