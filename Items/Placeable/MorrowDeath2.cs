﻿using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
    public class MorrowDeath2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Staby");
			// Tooltip.SetDefault("Thing");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Structures.MorrowDeath2S>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}
	}
}