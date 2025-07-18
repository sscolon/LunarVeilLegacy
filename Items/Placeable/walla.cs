﻿using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
    public class walla : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("wall1");
			// Tooltip.SetDefault("Thinga");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Structures.wall1>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}
	}
}