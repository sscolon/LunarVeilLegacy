﻿using LunarVeilLegacy.Tiles.RoyalCapital;
using LunarVeilLegacy.Tiles.DrakonicManor;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable.Manor
{
    public class DrakonicHallI : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("CurtainLeft");
			// Tooltip.SetDefault("Curtain");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<DrakonHallS>());
			Item.value = 150;
			Item.maxStack = 99;
			Item.width = 38;
			Item.height = 24;
		}
	}
}