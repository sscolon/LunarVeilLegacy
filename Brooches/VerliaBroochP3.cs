﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Brooches
{
    public class VerliaBroochP3 : ModProjectile
    {

        public override void SetDefaults()
        {

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = false;
            Projectile.height = 35;
            Projectile.width = 35;
            Projectile.penetrate = 20;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        public override bool PreAI()
        {
            Projectile.alpha++;

            float num = 1f - Projectile.alpha / 255f;
            Projectile.velocity *= .98f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            num *= Projectile.scale;
            Lighting.AddLight(Projectile.Center, Color.LightSkyBlue.ToVector3() * 1.25f * Main.essScale);
            Projectile.rotation = Projectile.velocity.X / 2f;
            return true;
        }
        float alphaCounter;
        public override void AI()
        {
            alphaCounter += 0.04f;
            Projectile.rotation += 0.3f;
            Projectile.scale *= 0.99f;



            if (Projectile.scale <= 0)
            {
                Projectile.Kill();
            }
        }

        
    }
}
