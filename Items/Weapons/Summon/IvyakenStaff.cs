
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Buffs.Minions;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Projectiles.Summons.Minions;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static LunarVeilLegacy.Items.Weapons.Summon.IvyakenStaff;
using static Terraria.ModLoader.ModContent;

namespace LunarVeilLegacy.Items.Weapons.Summon
{
	public class IvyakenStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ivyaken Staff");
			// Tooltip.SetDefault("Summons an Ivyaken to fight for you");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.knockBack = 3f;
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<IvyakenMinionBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<IvyakenMinionProj>();
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Ivythorn>(), 9);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            position = Main.MouseWorld;
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }
	}
}