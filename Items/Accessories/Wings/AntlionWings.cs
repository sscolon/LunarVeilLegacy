﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Projectiles.Wings;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class AntlionWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			// These wings use the same values as the solar wings
			// Fly time: 180 ticks = 3 seconds
			// Fly speed: 9
			// Acceleration multiplier: 2.5
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(120, 6f, 1.1f);

        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 1;
			Item.rare = ItemRarityID.LightRed;
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
			player.slowFall = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<AntlionWingsProj>()] == 0)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero,
					ModContent.ProjectileType<AntlionWingsProj>(), 0, 0, player.whoAmI);
			}
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f; // Falling glide speed
			ascentWhenRising = 0.3f; // Rising speed
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 5f;
			constantAscend = 0.135f;
		}
	}
}
