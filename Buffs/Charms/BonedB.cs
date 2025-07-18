﻿using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Buffs.Charms
{
	public class BonedB : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charm Buff!");
			// Description.SetDefault("2 extra defense");
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 2;
			player.moveSpeed += 0.1f;
			player.maxRunSpeed += 0.2f;
			player.GetAttackSpeed(DamageClass.Throwing) += 0.1f;
		}
	}
}