﻿using LunarVeilLegacy.Items.Accessories.Brooches;
using Terraria;

namespace LunarVeilLegacy.Items.Flasks
{
    public class VialedInsourceProj : InsourceDefaultProjectile
    {
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            FlaskPlayer FlaskPlayer = owner.GetModPlayer<FlaskPlayer>();
            if (!FlaskPlayer.hasVialedInsource)
            {
                Projectile.Kill();
                return;
            }

            base.AI();
        }
    }
}