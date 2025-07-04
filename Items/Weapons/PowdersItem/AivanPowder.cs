﻿using LunarVeilLegacy.Projectiles.Powders;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace LunarVeilLegacy.Items.Weapons.PowdersItem
{
    internal class AivanPowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sepsis Powder");
			/* Tooltip.SetDefault("Throw magical dust on them!" +
				"\nA sparkly star dust that does double damage as the igniter!"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<AivanPowderProj>();
			Item.autoReuse = true;
			Item.shootSpeed = 12f;
			Item.crit = 57;
			Item.UseSound = SoundID.Grass;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity *= player.GetModPlayer<MyPlayer>().IgniterVelocity, type, damage, knockback, player.whoAmI);
			return false;
		}
	}
}