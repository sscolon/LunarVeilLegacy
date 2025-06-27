using Terraria;

namespace LunarVeilLegacy.Items.Flasks
{
    public class VitalityInsourceProj : InsourceDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            FlaskPlayer FlaskPlayer = owner.GetModPlayer<FlaskPlayer>();
            if (!FlaskPlayer.hasVitalityInsource)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}