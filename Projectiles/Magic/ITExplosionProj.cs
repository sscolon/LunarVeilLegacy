﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using LunarVeilLegacy.Dusts;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Trails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using LunarVeilLegacy.UI.Systems;
using LunarVeilLegacy.Buffs;

namespace LunarVeilLegacy.Projectiles.Magic
{
    internal class ITExplosionProj : ModProjectile,
        IPixelPrimitiveDrawer
    {
        //Texture
        public override string Texture => TextureRegistry.EmptyTexture;

        //AI
        private float LifeTime => 32f;
        private ref float Timer => ref Projectile.ai[0];
        private ref float DelayTimer => ref Projectile.ai[1];
        private float Progress
        {
            get
            {
                float p = Timer / LifeTime;
                return MathHelper.Clamp(p, 0, 1);
            }
        }

        //Draw Code
        private PrimitiveTrail BeamDrawer;
        private int DrawMode;
        private bool SpawnDustCircle;

        //Trailing
        private Asset<Texture2D> FrontTrailTexture => TrailRegistry.WaterTrail;
        private MiscShaderData FrontTrailShader => TrailRegistry.LaserShader;

        private Asset<Texture2D> BackTrailTexture => TrailRegistry.WhispyTrail;
        private MiscShaderData BackTrailShader => TrailRegistry.LaserShader;

        //Radius
        private float StartRadius => 4;
        private float EndRadius => Main.rand.NextFloat(100, 100);
        private float Width => Main.rand.NextFloat(32, 64);

        //Colors
        private Color FrontCircleStartDrawColor => Color.White;
        private Color FrontCircleEndDrawColor => Color.DarkSeaGreen;
        private Color BackCircleStartDrawColor => Color.Lerp(Color.White, Color.GreenYellow, 0.4f);
        private Color BackCircleEndDrawColor => Color.Lerp(Color.GreenYellow, Color.DarkSeaGreen, 0.7f);
        private Vector2[] CirclePos;

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.timeLeft = (int)LifeTime;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            //Points on the circle
            CirclePos = new Vector2[64];
        }

        public override void AI()
        {
            if (DelayTimer > 0)
            {
                Projectile.friendly = false;
                Projectile.timeLeft = (int)LifeTime;
                DelayTimer--;
                return;
            }

            Projectile.friendly = true;
            Timer++;
            if (Timer == 1)
            {
                Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Projectile.Center, 1024, 16f);
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<TSmokeDust>(), (Vector2.One * Main.rand.Next(1, 5)).RotatedByRandom(19.0), 0, Color.DarkGray, 1f).noGravity = true;
                }
            }

            AI_ExpandCircle();
            AI_DustCircle();
        }

        private void AI_ExpandCircle()
        {
            float easedProgess = Easing.InOutCirc(Progress);
            float radius = MathHelper.Lerp(StartRadius, EndRadius, easedProgess);
            DrawCircle(radius);
        }

        private void AI_DustCircle()
        {
            if (!SpawnDustCircle && Timer >= 15)
            {
                for (int i = 0; i < 48; i++)
                {
                    Vector2 rand = Main.rand.NextVector2CircularEdge(EndRadius, EndRadius);
                    Vector2 pos = Projectile.Center + rand;
                    Dust d = Dust.NewDustPerfect(pos, ModContent.DustType<GlowDust>(), Vector2.Zero,
                        newColor: BackCircleStartDrawColor,
                        Scale: Main.rand.NextFloat(0.3f, 0.6f));
                    d.noGravity = true;
                }
                SpawnDustCircle = true;
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        private void DrawCircle(float radius)
        {
            Vector2 startDirection = Vector2.UnitY;
            for (int i = 0; i < CirclePos.Length; i++)
            {
                float circleProgress = i / (float)CirclePos.Length;
                float radiansToRotateBy = circleProgress * (MathHelper.TwoPi + MathHelper.PiOver4 / 2);
                CirclePos[i] = Projectile.Center + startDirection.RotatedBy(radiansToRotateBy) * radius;
            }
        }

        public float WidthFunction(float completionRatio)
        {
            float width = Width;
            float startExplosionScale = 4f;
            float endExplosionScale = 0f;
            float easedProgess = Easing.OutCirc(Progress);
            float scale = MathHelper.Lerp(startExplosionScale, endExplosionScale, easedProgess);
            switch (DrawMode)
            {
                default:
                case 0:
                    return Projectile.scale * scale * width * Easing.SpikeInOutCirc(Progress);
                case 1:
                    return Projectile.scale * width * 2.2f * Easing.SpikeInOutCirc(Progress);

            }
        }

        public Color ColorFunction(float completionRatio)
        {
            switch (DrawMode)
            {
                default:
                case 0:
                    //Front Trail
                    return Color.Transparent;
                case 1:
                    //Back Trail
                    return Color.Transparent;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AcidFlame>(), 200);
        }

        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
            BeamDrawer ??= new PrimitiveTrail(WidthFunction, ColorFunction, null, true, TrailRegistry.LaserShader);
            float easedProgess = Easing.OutCubic(Progress);

            //Back Trail   
            DrawMode = 1;
            BeamDrawer.SpecialShader = BackTrailShader;
            BeamDrawer.SpecialShader.UseColor(
                Color.Lerp(BackCircleStartDrawColor, BackCircleEndDrawColor, easedProgess));
            BeamDrawer.SpecialShader.SetShaderTexture(BackTrailTexture);
            BeamDrawer.DrawPixelated(CirclePos, -Main.screenPosition, CirclePos.Length);

            //Front Trail
            DrawMode = 0;
            BeamDrawer.SpecialShader = FrontTrailShader;
            BeamDrawer.SpecialShader.UseColor(Color.Lerp(FrontCircleStartDrawColor, FrontCircleEndDrawColor,
                Easing.OutCirc(Progress)));
            BeamDrawer.SpecialShader.SetShaderTexture(FrontTrailTexture);
            BeamDrawer.DrawPixelated(CirclePos, -Main.screenPosition, CirclePos.Length);
            Main.spriteBatch.ExitShaderRegion();
        }
    }
}
