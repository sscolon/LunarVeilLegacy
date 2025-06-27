﻿using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Buffs.Charms
{
    public class BearB : ModBuff
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
			player.manaRegen += 2;
			player.statDefense += 4;
		}
	}
}