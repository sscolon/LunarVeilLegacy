﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Weapons.Melee;
using LunarVeilLegacy.Items.Weapons.Melee.Shields;
using LunarVeilLegacy.Items.Weapons.PowdersItem;
using LunarVeilLegacy.Items.Weapons.Ranged;
using LunarVeilLegacy.Items.Weapons.Summon;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Consumables
{
    public class VeribossBag : ModItem
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Veriplant Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"); // References a language key that says "Right Click To Open" in the language of the game

			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Purple;
			
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			// We have to replicate the expert drops from MinionBossBody here via QuickSpawnItem

			var entitySource = player.GetSource_OpenItem(Type);

			if (Main.rand.NextBool(1))
			{
				switch (Main.rand.Next(6))
				{


					case 0:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<Verstidust>());

						break;
					case 1:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<VerstiDance>());

						break;
					case 2:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<Verstibloom>());

						break;
					case 3:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<SteamedNail>());

						break;
					case 4:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<Hornet>());

						break;
					case 5:

						player.QuickSpawnItem(entitySource, ModContent.ItemType<StarCall>());

						break;
				}
			}
			

			if (Main.rand.NextBool(1))
			{
				player.QuickSpawnItem(entitySource, ItemID.GoldCoin, Main.rand.Next(5, 13));
			}

		}

		// Below is code for the visuals

		public override Color? GetAlpha(Color lightColor)
		{
			// Makes sure the dropped bag is always visible
			return Color.Lerp(lightColor, Color.White, 0.4f);
		}

		public override void PostUpdate()
		{
			// Spawn some light and dust when dropped in the world
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

			if (Item.timeSinceItemSpawned % 12 == 0)
			{
				Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

				// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
				Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
				float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
				Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

				Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
				dust.alpha = 0;
			}
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
	}
}