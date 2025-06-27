using LunarVeilLegacy.Items.Accessories.Brooches;
using Terraria;

namespace LunarVeilLegacy.Brooches
{
    public class SandyBrooch : BroochDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            BroochPlayer broochPlayer = owner.GetModPlayer<BroochPlayer>();
            if (!broochPlayer.hasSandyBrooch)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}