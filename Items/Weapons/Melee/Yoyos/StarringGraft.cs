﻿
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Projectiles.Thrown;
using LunarVeilLegacy.Projectiles.Yoyo;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Melee.Yoyos
{
	public class StarringGraft : ModItem
	{
		public override void SetStaticDefaults()
		{


			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 35;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 24; // The width of the item's hitbox.
			Item.height = 24; // The height of the item's hitbox.

			Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
			Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
			Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
			Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
			Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
			Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

			Item.damage = 26; // The amount of damage the item does to an enemy or player.
			Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
			Item.knockBack = 3f; // The amount of knockback the item inflicts.
			Item.crit = 16; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
			Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
			Item.rare = ItemRarityID.Blue; // The item's rarity. This changes the color of the item's name.
			Item.value = Item.buyPrice(gold: 1); // The amount of money that the item is can be bought for.

			Item.shoot = ModContent.ProjectileType<StarringGraftHead>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
			Item.shootSpeed = 16f; // The velocity of the shot projectile.			
		}

		// Here is an example of blacklisting certain modifiers. Remove this section for standard vanilla behavior.
		// In this example, we are blacklisting the ones that reduce damage of a melee weapon.
		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes).
		private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

		public override bool AllowPrefix(int pre)
		{
			// return false to make the game reroll the prefix.

			// DON'T DO THIS BY ITSELF:
			// return false;
			// This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

			if (Array.IndexOf(unwantedPrefixes, pre) > -1)
			{
				// IndexOf returns a positive index of the element you search for. If not found, it's less than 0.
				// Here we check if the selected prefix is positive (it was found).
				// If so, we found a prefix that we don't want. Reroll.
				return false;
			}

			// Don't reroll
			return true;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.Anvils);

			recipe.AddIngredient(ModContent.ItemType<FrileBar>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DreadFoil>(), 5);
			recipe.AddIngredient(ModContent.ItemType<AuroreanStarI>(), 50);
			recipe.AddIngredient(ItemID.WoodYoyo, 1);
			recipe.Register();
		}

	}
}