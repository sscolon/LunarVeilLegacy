﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Tiles.Structures.Cathedral;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable.Cathedral
{
    public class Orb1I : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Orb");
			// Tooltip.SetDefault("A giant crystal");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Orb1>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrystalShard, 30);
			recipe.AddIngredient(ModContent.ItemType<AlcadizMetal>(), 1);
			recipe.AddTile(TileID.Hellforge);
			recipe.Register();
		}
	}
}