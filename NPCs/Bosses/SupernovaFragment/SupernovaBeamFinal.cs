using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Effects;
using LunarVeilLegacy.Trails;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Bosses.SupernovaFragment
{
    public class SupernovaBeamFinal : ModProjectile, IPixelPrimitiveDrawer
    {
        internal PrimitiveTrailCopy BeamDrawer;
        public ref float Time => ref Projectile.ai[0];
        public NPC Owner => Main.npc[(int)Projectile.ai[1]];
        public const float LaserLength = 7400f;

        public override void SetDefaults()
        {
            CooldownSlot = ImmunityCooldownID.Bosses;
            Projectile.width = Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 15;
            Projectile.alpha = 255;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }
        public override bool ShouldUpdatePosition()
        {
            //Returning false makes velocity not move the projectile
            return false;
        }

        public override void AI()
        {
            // Fade in.
            Projectile.alpha = Utils.Clamp(Projectile.alpha - 25, 0, 255);
            Projectile.scale = MathF.Sin(Time / 15f * MathHelper.Pi) * 3f;
            if (Projectile.scale > 1f)
            {
                Projectile.scale = 1f;

            }

            Projectile.position = SupernovaFragment.SingularityPos;
            float rot = Owner.rotation - MathHelper.PiOver2;

            Projectile.velocity = rot.ToRotationVector2();

            Projectile.position -= Projectile.velocity * 1200;
            // And create bright light.
            Lighting.AddLight(Projectile.Center, Color.Red.ToVector3() * 1.4f);
            Time++;
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float width = Projectile.width * 0.5f;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity * (LaserLength - 80f);
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, width, ref _);
        }

        public float WidthFunction(float completionRatio)
        {
            return Projectile.width * Projectile.scale * 2f;
        }


        public Color ColorFunction(float completionRatio)
        {
            Color color = Color.Lerp(Color.White, Color.White, 0.65f);
            return color * Projectile.Opacity * MathF.Pow(Utils.GetLerpValue(0f, 0.1f, completionRatio, true), 3f);
        }

        public override bool PreDraw(ref Color lightColor) => false;

        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            BeamDrawer ??= new PrimitiveTrailCopy(WidthFunction, ColorFunction, null, true, TrailRegistry.LaserShader);

            Color middleColor = Color.Lerp(Color.White, Color.White, 0.6f);
            Color middleColor2 = Color.Lerp(Color.White, Color.OrangeRed, 0.5f);
            Color finalColor = Color.Lerp(middleColor, middleColor2, Time / 120);

            TrailRegistry.LaserShader.UseColor(Color.OrangeRed);
            TrailRegistry.LaserShader.SetShaderTexture(TrailRegistry.WaterTrail);

            List<float> originalRotations = new();
            List<Vector2> points = new();
            for (int i = 0; i <= 8; i++)
            {
                points.Add(Vector2.Lerp(Projectile.Center, Projectile.Center + Projectile.velocity * LaserLength, i / 8f));
                originalRotations.Add(MathHelper.PiOver2);
            }

            BeamDrawer.DrawPixelated(points, -Main.screenPosition, 32);
            Main.spriteBatch.ExitShaderRegion();
        }

        public override bool? CanDamage() => Time >= 8f;
    }
}