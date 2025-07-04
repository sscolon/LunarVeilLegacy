﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Projectiles.Powders;
using LunarVeilLegacy.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace LunarVeilLegacy.Items.Weapons.PowdersItem
{
    internal class EreshkinPowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sepsis Powder");
			/* Tooltip.SetDefault("Throw magical dust on them!" +
				"\nA sparkly star dust that does double damage as the igniter!"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<IshyPowderProj>();
			Item.autoReuse = true;
			Item.shootSpeed = 12f;
			Item.crit = 2;
			Item.UseSound = SoundID.Grass;
		}
	
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			int dir = player.direction;

			Projectile.NewProjectile(source, position, velocity *= player.GetModPlayer<MyPlayer>().IgniterVelocity, type, damage, knockback, player.whoAmI);
			return false;
		}

	}
}