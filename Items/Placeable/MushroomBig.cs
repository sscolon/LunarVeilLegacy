﻿using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
    public class MushroomBig : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("MushroomBig");
			// Tooltip.SetDefault("Thing");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Structures.MushroomBigS>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}
	}
}