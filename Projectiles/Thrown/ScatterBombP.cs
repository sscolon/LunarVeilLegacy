﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Projectiles.IgniterExplosions;
using LunarVeilLegacy.Trails;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Thrown
{
    internal class ScatterbombP : ModProjectile
    {
        private float _rotation;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 3600;
        }

        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 0.5f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }

        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.OrangeRed, Color.Transparent, completionRatio);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawHelper.DrawSimpleTrail(Projectile, WidthFunction, ColorFunction, TrailRegistry.VortexTrail);
            DrawHelper.DrawAdditiveAfterImage(Projectile, Color.OrangeRed, Color.Transparent, ref lightColor);
            return base.PreDraw(ref lightColor);
        }

        public override void AI()
        {
            Projectile.rotation += _rotation;
            _rotation += 0.01f;

            Vector3 RGB = new(1.00f, 0.37f, 0.30f);
            // The multiplication here wasn't doing anything
            Lighting.AddLight(Projectile.position, RGB.X, RGB.Y, RGB.Z);

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f);
            Main.dust[dust].scale = 0.6f;
        }


        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0, -120), Vector2.Zero,
                ModContent.ProjectileType<ScatterBoom>(), Projectile.damage * 3, Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
            Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Projectile.Center, 1024f, 32f);
            for (int i = 0; i < Main.rand.Next(3, 7); i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(16f, 16f);
                int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                    ProjectileID.GreekFire3, Projectile.damage, 0f, Projectile.owner);
                Main.projectile[index].friendly = true;
                Main.projectile[index].hostile = false;
            }
        }
    }
}
