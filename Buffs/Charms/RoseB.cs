﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Buffs.Charms
{
    public class RoseB : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charm Buff!");
			// Description.SetDefault("Icy Frileness!");
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Lighting.AddLight(player.Center, Color.LightYellow.ToVector3() * 2.75f * Main.essScale);
			player.statDefense += 2;
			player.pickSpeed /= 1.1f;
			player.noFallDmg = true;
			player.GetDamage(DamageClass.Generic) *= 0.85f;
		}
	}
}