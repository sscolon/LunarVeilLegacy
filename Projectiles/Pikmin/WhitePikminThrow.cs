﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Trails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Pikmin
{
    internal class WhitePikminThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 20;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 19;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.scale = 1.2f;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Visuals();
            Projectile.tileCollide = true;
        }

        private void Visuals()
        {

            if (Main.rand.NextBool(60))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WhiteTorch);
            }
        }

        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 0.5f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }

        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.White * 0.1f, Color.Transparent, completionRatio);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawHelper.DrawSimpleTrail(Projectile, WidthFunction, ColorFunction, TrailRegistry.BulbTrail);
            return base.PreDraw(ref lightColor);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int targetNpc = target.whoAmI;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity + new Vector2(0.001f, 0.001f),
                ModContent.ProjectileType<WhitePikminAttack>(), Projectile.damage, Projectile.knockBack, Projectile.owner, ai0: targetNpc);
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 0,
              ModContent.ProjectileType<NailKaboom>(), Projectile.damage * 0, Projectile.knockBack, Projectile.owner);
            return base.OnTileCollide(oldVelocity);
        }

        public override bool PreAI()
        {

            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 20)
                {
                    Projectile.frame = 19;
                }
            }
            return true;


        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2f, 2f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.WhiteTorch, speed * 4);
                d.noGravity = true;
            }
        }
    }
}
