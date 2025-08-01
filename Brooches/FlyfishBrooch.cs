﻿using Terraria;

namespace LunarVeilLegacy.Brooches
{
    public class FlyfishBrooch : BroochDefaultProjectile
	{
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            BroochPlayer broochPlayer = owner.GetModPlayer<BroochPlayer>();
            if (!broochPlayer.hasFlyfishBrooch)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}