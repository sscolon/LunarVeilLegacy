﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
    public class Flowersummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Morrowed Flower Batch");
			// Tooltip.SetDefault("Do not right click or disturb the evil");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.FlowerSummon>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}

		
	}
}