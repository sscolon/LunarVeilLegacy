using LunarVeilLegacy.Items.Accessories.Brooches;
using Terraria;

namespace LunarVeilLegacy.Brooches
{
    public class LuckyWinnerBrooch : BroochDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            BroochPlayer broochPlayer = owner.GetModPlayer<BroochPlayer>();
            if (!broochPlayer.hasLuckyWBrooch || !broochPlayer.hasAdvancedBrooches)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}