﻿using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Thrown
{
	internal class LarvaedSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pearlescent Ice Ball");
			// Tooltip.SetDefault("Shoots fast homing sparks of light!");
		}
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.width = 40;
			Item.height = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.DamageType = DamageClass.Throwing;
			Item.value = 200;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.DD2_DarkMageAttack;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LarvaeSpearP>();
			Item.shootSpeed = 20f;
			Item.autoReuse = true;
			Item.crit = 12;
			Item.noUseGraphic = true;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
		}


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(ItemID.BorealWood, 10);
			recipe.AddIngredient(ModContent.ItemType<RippedFabric>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EldritchSoul>(), 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

		}
	}
}









