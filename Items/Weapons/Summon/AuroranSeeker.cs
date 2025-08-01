﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Buffs.Minions;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Projectiles.Summons.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Summon
{
    public class AuroranSeeker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Alcadiz String n Charm");
			/* Tooltip.SetDefault("Your summons will target focused enemies" +
				"\nSummons will manifest out of your string shield" +
				"\nThey will act as temporary summons to give your other minions company!" +
				"\nThe Charm above gives you 10+ Defense while active!"); */
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Blue;
			Item.value = Terraria.Item.sellPrice(0, 10, 80, 0);
			Item.CloneDefaults(ItemID.Arkhalis);
			Item.damage = 9; // Sets the Item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.DamageType = DamageClass.Summon;
			Item.mana = 20;
			Item.useTime = 90; // The Item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 30; // The length of the Item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the Item's animation doesn't do damage
			Item.knockBack = 4; // Sets the Item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.value = 10000; // how much the Item sells for (measured in copper)
			Item.UseSound = SoundID.AbigailSummon; // The sound that this Item plays when used.
			Item.autoReuse = true; // if you can hold click to automatically use it again
			Item.shoot = ModContent.ProjectileType<AuroranSeekerMinionProj>();
			Item.shootSpeed = 0f; // the speed of the projectile (measured in pixels per frame)
			Item.channel = true;
			Item.buffType = ModContent.BuffType<AuroranSeekerMinionBuff>();
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Lighting.AddLight(Item.position, 0.46f, .07f, .52f);
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

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			position = Main.MouseWorld;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.Anvils);
			recipe.AddIngredient(ItemID.Chain, 10);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddIngredient(ModContent.ItemType<FrileBar>(), 15);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.Register();
		}
	}
}