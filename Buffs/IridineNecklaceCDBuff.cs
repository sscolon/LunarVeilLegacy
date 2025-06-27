using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Buffs
{
    internal class IridineNecklaceCDBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
