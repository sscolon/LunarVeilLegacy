﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Buffs;
using LunarVeilLegacy.Buffs.Whipfx;
using LunarVeilLegacy.Dusts;
using LunarVeilLegacy.Gores;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Trails;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Summons.Orbs
{
    internal class ThePollinatorProj : OrbProjectile
    {
        public enum ActionState
        {
            Orbit,
            Swing_1,
            Swing_2,
            Swing_3
        }

        public const float Swing_Time = 40 * Swing_Speed_Multiplier;
        public const float Swing_Time_2 = 60 * Swing_Speed_Multiplier;
        public const float Final_Swing_Distance = 252;
        public const float Combo_Time = 8;
        public const int Swing_Speed_Multiplier = 8;

        public override float MaxThrowDistance => 462;

        ref float ComboCounter => ref Projectile.ai[0];
        public ActionState State
        {
            get => (ActionState)Projectile.ai[1];
            set => Projectile.ai[1] = (float)value;
        }

        ref float Timer => ref Projectile.ai[2];
        float SwingTime;
        float EasedProgress;
        Vector2 SwingStart;
        Vector2 SwingTarget;
        Vector2 SwingVelocity;
        int DustTimer;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WriteVector2(SwingStart);
            writer.WriteVector2(SwingTarget);
            writer.WriteVector2(SwingVelocity);
            writer.Write(SwingTime);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            SwingStart = reader.ReadVector2();
            SwingTarget = reader.ReadVector2();
            SwingVelocity = reader.ReadVector2();
            SwingTime = reader.ReadSingle();
        }


        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 32;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 52;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.timeLeft = int.MaxValue;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.extraUpdates = Swing_Speed_Multiplier;
        }

        public override bool? CanDamage()
        {
            //Only deal damage while swinging
            return State != ActionState.Orbit;
        }

        public override void AI()
        {
            //Kill yourself if not holding the item
            if (!Owner.HasBuff(ModContent.BuffType<OrbMaster>()))
            {
                Projectile.Kill();
                return;
            }

            switch (State)
            {
                case ActionState.Orbit:
                    Orbit();
                    break;
                case ActionState.Swing_1:
                    Swing1();
                    break;
                case ActionState.Swing_2:
                    Swing2();
                    break;
                case ActionState.Swing_3:
                    Swing3();
                    break;
            }
        }


        private void Orbit()
        {
            //Orbiting
            float orbitSpeed = 5;
            float orbitDistance = 48;
            float orbitProgress = VectorHelper.Osc(0, 1, orbitSpeed);
            float easedProgress = Easing.InOutCubic(orbitProgress);

            //Hovering
            float hoverSpeed = 5;
            float hoverDistance = 24;
            float yOffset = VectorHelper.Osc(-hoverDistance, hoverDistance, hoverSpeed);

            Vector2 offset = new Vector2(orbitDistance, yOffset);
            Vector2 startVector = Owner.Center - offset;
            Vector2 endVector = Owner.Center + offset;

            //Lerp
            Vector2 targetCenter = Vector2.Lerp(startVector, endVector, easedProgress);
            Projectile.Center = Vector2.Lerp(Projectile.Center, targetCenter, 0.12f / Swing_Speed_Multiplier);
            if (ComboCounter >= 1)
            {
                //Throw Sounds
                SoundStyle soundStyle = SoundID.Item7;
                soundStyle.PitchVariance = 0.15f;
                SoundEngine.PlaySound(soundStyle, Projectile.position);
                SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Morrowarrow") { PitchVariance = 0.15f}, Projectile.position);

                OrbHelper.PlaySummonSound(Projectile.position);
                Reset();
                SwingVelocity = Owner.DirectionTo(SwingTarget);
                SwingStart = Owner.Center;
                if (Main.myPlayer == Projectile.owner)
                {
                    SwingTarget = GetSwingTarget();
                    Projectile.netUpdate = true;
                }

                SwingTime = Swing_Time;
                State = ActionState.Swing_1;
                ComboCounter = 0;
                Timer = 0;
            }
        }

        private void Reset()
        {
            EasedProgress = 0;
            for (int i = 0; i < Projectile.localNPCImmunity.Length; i++)
            {
                Projectile.localNPCImmunity[i] = 0;
            }
        }

        private void SwingDusts()
        {
            DustTimer++;
            if (DustTimer >= 4 * Swing_Speed_Multiplier)
            {
                DustTimer = 0;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<GunFlash>(), newColor: Color.White, Scale: 0.8f);
            }
        }

        private void Swing1()
        {
            SwingDusts();
            Timer++;

            float progress = Timer / SwingTime;
            EasedProgress = Easing.SpikeInOutExpo(progress);

            Vector2 start = Owner.Center;
            Vector2 end = SwingTarget + (SwingTarget - start).SafeNormalize(Vector2.Zero) * Final_Swing_Distance;
            Vector2 lerpPosition = Vector2.Lerp(start, end, EasedProgress);

            Projectile.Center = Vector2.Lerp(Projectile.Center, lerpPosition, 0.54f / Swing_Speed_Multiplier);
            if (Timer > SwingTime)
            {
                if (ComboCounter >= 1)
                {
                    //Throw Sounds
                    SoundStyle soundStyle = SoundID.Item7;
                    soundStyle.PitchVariance = 0.15f;
                    SoundEngine.PlaySound(soundStyle, Projectile.position);
                    SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Morrowarrow") { PitchVariance = 0.15f }, Projectile.position);
                    OrbHelper.PlaySummonSound(Projectile.position);
                    Reset();
                    SwingVelocity = Owner.DirectionTo(SwingTarget);
                    float distance = 180;
                    SwingStart = Owner.Center + SwingVelocity.RotatedByRandom(MathHelper.TwoPi) * distance;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        SwingTarget = GetSwingTarget();
                        Projectile.netUpdate = true;
                    }

                    SwingTime = Swing_Time;
                    State = ActionState.Swing_2;
                    ComboCounter = 0;
                    Timer = 0;
                }
                else if (Timer > SwingTime + Combo_Time)
                {
                    ComboCounter = 0;
                    Timer = 0;
                    State = ActionState.Orbit;
                }
            }
        }

        private void Swing2()
        {
            SwingDusts();
            Timer++;

            float progress = Timer / SwingTime;
            EasedProgress = Easing.SpikeInOutExpo(progress);

            Vector2 start = Owner.Center;
            Vector2 end = SwingTarget + (SwingTarget - start).SafeNormalize(Vector2.Zero) * Final_Swing_Distance;
            Vector2 lerpPosition = Vector2.Lerp(start, end, EasedProgress);

            Projectile.Center = Vector2.Lerp(Projectile.Center, lerpPosition, 0.54f / Swing_Speed_Multiplier);
            if (Timer > SwingTime)
            {
                if (ComboCounter >= 1)
                {
                    //Throw Sounds
                    SoundStyle soundStyle = SoundID.Item7;
                    soundStyle.PitchVariance = 0.15f;
                    soundStyle.Pitch = -0.25f;

                    SoundEngine.PlaySound(soundStyle, Projectile.position);
                    SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Morrowarrow") { PitchVariance = 0.15f, Pitch = -0.25f }, Projectile.position);
                    OrbHelper.PlaySummonSound(Projectile.position);
                    Reset();
                    SwingVelocity = Owner.DirectionTo(SwingTarget);
                    SwingStart = Projectile.Center;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        SwingTarget = GetSwingTarget();
                        Projectile.netUpdate = true;
                    }

                    SwingTime = Swing_Time_2;
                    State = ActionState.Swing_3;
                    ComboCounter = 0;
                    Timer = 0;
                }
                else if (Timer > SwingTime + Combo_Time)
                {
                    ComboCounter = 0;
                    Timer = 0;
                    State = ActionState.Orbit;
                }
            }
        }

        private void Swing3()
        {
            SwingDusts();
            Timer++;
            float progress = Timer / SwingTime;
            EasedProgress = Easing.SpikeInOutExpo(progress);

            Vector2 start = Owner.Center;
            Vector2 end = SwingTarget + (SwingTarget - start).SafeNormalize(Vector2.Zero) * Final_Swing_Distance;
            Vector2 lerpPosition = Vector2.Lerp(start, end, EasedProgress);

            Projectile.Center = Vector2.Lerp(Projectile.Center, lerpPosition, 0.54f / Swing_Speed_Multiplier);
            if (Timer > SwingTime)
            {
                ComboCounter = 0;
                Timer = 0;
                State = ActionState.Orbit;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(ModContent.BuffType<ThePollinatorDebuff>(), 240);
            SoundStyle soundStyle;

            int gore1 = GoreHelper.TypeFallingLeafWhite;
            int gore2 = GoreHelper.TypeFallingLeafRed;
            for(int i = 0; i< 2; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(8, 8);
                Gore.NewGore(Projectile.GetSource_FromThis(), target.position, velocity, gore1);

                velocity = velocity.RotatedByRandom(MathHelper.TwoPi);
                Gore.NewGore(Projectile.GetSource_FromThis(), target.position, velocity, gore2);
            }

            switch (State)
            {
                case ActionState.Swing_1:
                case ActionState.Swing_2:
                    for (int i = 0; i < 4; i++)
                    {
                        Dust.NewDust(target.position, Projectile.width, Projectile.height, ModContent.DustType<GunFlash>(), Scale: 0.8f);
                        Dust.NewDustPerfect(target.position, ModContent.DustType<GlowDust>(), (Vector2.One * Main.rand.Next(1, 5)).RotatedByRandom(19.0), 0, Color.DarkGoldenrod, 1f).noGravity = true;
                    }

                    soundStyle = SoundID.Grass;
                    soundStyle.PitchVariance = 0.15f;
                    SoundEngine.PlaySound(soundStyle, Projectile.position);
                    SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/MorrowExp") { PitchVariance = 0.15f}, Projectile.position);
                    break;

                case ActionState.Swing_3:
                    for (int i = 0; i < 14; i++)
                    {
                        Dust.NewDustPerfect(target.Center, ModContent.DustType<TSmokeDust>(), (Vector2.One * Main.rand.Next(1, 5)).RotatedByRandom(19.0), 0, Color.DarkGoldenrod, 1f).noGravity = true;
                    }

                    for (int i = 0; i < 14; i++)
                    {
                      
                        Dust.NewDustPerfect(target.Center, ModContent.DustType<GlowDust>(), (Vector2.One * Main.rand.Next(1, 5)).RotatedByRandom(19.0), 0, Color.DarkGoldenrod, 1f).noGravity = true;
                    }

                    Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Projectile.Center, 1024f, 32f);
                    target.SimpleStrikeNPC(Projectile.damage, hit.HitDirection);
                    soundStyle = SoundID.Grass;
                    soundStyle.PitchVariance = 0.15f;
                    SoundEngine.PlaySound(soundStyle, Projectile.position);
                    SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/OverGrowth_Thorn1") { PitchVariance = 0.15f }, Projectile.position);
                    break;
            }
        }

        public TrailRenderer SwordSlash;
        public TrailRenderer SwordSlash2;
        public override bool PreDraw(ref Color lightColor)
        {
            Vector3 huntrianColorXyz = DrawHelper.HuntrianColorOscillate(
                Color.LightGoldenrodYellow.ToVector3(),
                Color.DarkGoldenrod.ToVector3(),
                new Vector3(3, 3, 3), 0);


            var TrailTex = ModContent.Request<Texture2D>("LunarVeilLegacy/Effects/Primitives/Trails/StarTrail").Value;
            var TrailTex2 = ModContent.Request<Texture2D>("LunarVeilLegacy/Effects/Primitives/Trails/WhispyTrail").Value;
            Color color = Color.Multiply(new(1.50f, 1.75f, 3.5f, 0), 200);
            if (SwordSlash == null)
            {
                SwordSlash = new TrailRenderer(TrailTex, TrailRenderer.DefaultPass,
                    (p) => Vector2.Lerp(new Vector2(28), new Vector2(12), p),
                    (p) => Color.White * (1f - p));
                SwordSlash.drawOffset = Projectile.Size / 2f;
            }
            if (SwordSlash2 == null)
            {
                SwordSlash2 = new TrailRenderer(TrailTex2, TrailRenderer.DefaultPass,
                    (p) => Vector2.Lerp(new Vector2(28), new Vector2(12), p),
                    (p) => Color.Goldenrod * (1f - p));
                SwordSlash2.drawOffset = Projectile.Size / 2f;
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            float[] rotation = new float[Projectile.oldRot.Length];
            for (int i = 0; i < rotation.Length; i++)
            {
                rotation[i] = Projectile.oldRot[i] - MathHelper.ToRadians(45);
            }

            SwordSlash.Draw(Projectile.oldPos, rotation);
            SwordSlash2.Draw(Projectile.oldPos, rotation);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin();


            DrawHelper.DrawDimLight(Projectile, huntrianColorXyz.X, huntrianColorXyz.Y, huntrianColorXyz.Z, ColorFunctions.MiracleVoid, lightColor, 1);
            DrawHelper.DrawAdditiveAfterImage(Projectile, Color.DarkGoldenrod, Color.Transparent, ref lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}
