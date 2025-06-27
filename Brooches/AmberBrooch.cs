using LunarVeilLegacy.Items.Accessories.Brooches;
using Terraria;

namespace LunarVeilLegacy.Brooches
{
    public class AmberBrooch : BroochDefaultProjectile
	{
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            BroochPlayer broochPlayer = owner.GetModPlayer<BroochPlayer>();
            if (!broochPlayer.hasAmberBrooch)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}