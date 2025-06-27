using Terraria;

namespace LunarVeilLegacy.Brooches
{
    public class RoseBrooch : BroochDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            BroochPlayer broochPlayer = owner.GetModPlayer<BroochPlayer>();
            if (!broochPlayer.hasRoseBrooch)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}