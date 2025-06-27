using LunarVeilLegacy.Items.Accessories.Brooches;
using Terraria;

namespace LunarVeilLegacy.Items.Flasks
{
    public class EpsidonInsourceProj : InsourceDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            FlaskPlayer FlaskPlayer = owner.GetModPlayer<FlaskPlayer>();
            if (!FlaskPlayer.hasEpsidonInsource)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}