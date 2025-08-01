﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Accessories.Wings
{
	public class SerpentHoverPlayer : ModPlayer
	{
		public bool hasSerWings;
		public override void ResetEffects()
		{
			hasSerWings = false;
		}

		public override void PostUpdateEquips()
		{
			if (!hasSerWings)
			{
				return;
			}

			if (Player.controlDown && Player.controlJump && !Player.mount.Active && Player.wingTime > 0)
			{
				Player.position.Y -= Player.velocity.Y;
				if (Player.velocity.Y > 0.1f)
					Player.velocity.Y = 0.1f;
				else if (Player.velocity.Y < -0.1f)
					Player.velocity.Y = -0.1f;
			}
		}
	}

	[AutoloadEquip(EquipType.Wings)]
	public class SerpentWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			// These wings use the same values as the solar wings
			// Fly time: 180 ticks = 3 seconds
			// Fly speed: 9
			// Acceleration multiplier: 2.5
			ItemID.Sets.ItemNoGravity[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(1800, 11f, 2f, true, hoverFlySpeedOverride: 18f);
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 38;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.LightPurple;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SerpentHoverPlayer>().hasSerWings = true;
			
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
            ascentWhenFalling = 0.85f; // Falling glide speed
            ascentWhenRising = 0.25f; // Rising speed
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 5f;
            constantAscend = 0.135f;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			DrawHelper.DrawGlow2InWorld(Item, spriteBatch, ref rotation, ref scale, whoAmI);
			return true;
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			//The below code makes this item hover up and down in the world
			//Don't forget to make the item have no gravity, otherwise there will be weird side effects
			float hoverSpeed = 5;
			float hoverRange = 0.2f;
			float y = VectorHelper.Osc(-hoverRange, hoverRange, hoverSpeed);
			Vector2 position = new Vector2(Item.position.X, Item.position.Y + y);
			Item.position = position;
		}



		
	}
}
