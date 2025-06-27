using Microsoft.Xna.Framework;
using LunarVeilLegacy.Brooches;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LunarVeilLegacy.Items.Flasks;

namespace LunarVeilLegacy.Buffs
{
	public class Sected : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charm Buff!");
			// Description.SetDefault("A true warrior such as yourself knows no bounds");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			


		}
	}
}