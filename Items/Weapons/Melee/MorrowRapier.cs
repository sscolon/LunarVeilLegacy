﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Melee
{
    public class MorrowRapier : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Poke poke-dont get the wrong idea of the weapon function-poke");

			ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			// Common Properties
			Item.value = Item.sellPrice(silver: 10); // The number and type of coins item can be sold for to an NPC

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 34; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 34; // The length of the item's use time in ticks (60 ticks == 1 second.)
			Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			// Weapon Properties
			Item.damage = 17;
			Item.knockBack = 10.5f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

			// Projectile Properties
			Item.shootSpeed = 1f; // The speed of the projectile measured in pixels per frame.
			Item.shoot = ModContent.ProjectileType<MorrowRapproj>(); // The projectile that is fired from this weapon
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-3f, -3f);
		}
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
		public override bool? UseItem(Player player)
		{
			// Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
			if (!Main.dedServ)
			{
				SoundEngine.PlaySound((SoundStyle)Item.UseSound, player.Center);
			}

			return null;
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 120);
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			recipe.AddIngredient(ModContent.ItemType<OvermorrowWood>(), 12);
		}
	}
}