﻿
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Materials
{
	public class IlluriaLeaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Super silk!");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.IlluriaLeafTile>();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(10);
			recipe.AddIngredient(ModContent.ItemType<IlluriaGrassBlock>(), 1);
			recipe.AddTile(TileID.LivingLoom);
			recipe.Register();
		}
	}
}