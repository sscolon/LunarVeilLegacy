﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Accessories.Igniter
{
    public class TomedDustingMagic : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Book of Wooden Illusion");
			/* Tooltip.SetDefault("Increased Regeneration!" +
				"\n +3% damage" +
				"\n Increases crit strike change by 5% "); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "ADBPau", "Creates a very good voidal explosion on dust explosions and constants!")
			{
				OverrideColor = new Color(80, 187, 124)

			};
			tooltips.Add(line);
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<TomedDustingFlames>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AlcaricMush>(), 50);
			recipe.AddIngredient(ModContent.ItemType<PearlescentScrap>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Superfragment>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DarkEssence>(), 15);
			recipe.AddIngredient(ItemID.SoulofFright, 20);
			recipe.AddTile(ModContent.TileType<AlcaologyTable>());
			recipe.Register();
		}


		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().MagicTomeDusts = true;
		}
	}
}