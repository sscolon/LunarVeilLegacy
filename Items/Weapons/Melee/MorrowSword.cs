﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Melee
{
    public class MorrowSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("This sword feels warm, what is this Materials?"); // The (English) text shown below your weapon's name.

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 40; // The item texture's width.
			Item.height = 40; // The item texture's height.

			Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
			Item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.

			Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
			Item.damage = 8; // The damage your item deals.
			Item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
			Item.crit = 12; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

			Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.

			Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				// Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CursedTorch);
			}
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 20);
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddIngredient(ModContent.ItemType<OvermorrowWood>(), 12);
			recipe.Register();
		}
	}
}