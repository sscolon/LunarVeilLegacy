using LunarVeilLegacy.Items.Weapons.Melee;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Buffs
{
    internal class VixylDodgeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
