﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Trails;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace LunarVeilLegacy.Projectiles.Slashers.Swingers
{
    internal class VulcanBreakerSwingProj : ModProjectile
    {
        public override string Texture => "LunarVeilLegacy/Items/Weapons/Melee/VulcanBreaker";
        //DrawCode
        private TrailRenderer SwordSlash;
        private bool _exploded;

        //AI
        private ref float Timer => ref Projectile.ai[0];
        private ref float Dir => ref Projectile.ai[1];
        private Player Owner => Main.player[Projectile.owner];

        private float ExtraUpdates => 16;
        private float SwingTime => 32 / Owner.GetTotalAttackSpeed(Projectile.DamageType) * ExtraUpdates;
        private float SwingRange => MathHelper.Pi + MathHelper.PiOver2 + MathHelper.PiOver4;
        private float SwingYRadius => 32;
        private float SwingXRadius => 128;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 68;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 72;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

            Projectile.usesLocalNPCImmunity = true;

            //Set this to negative 1 so it only hits each npc once
            Projectile.localNPCHitCooldown = -1;

            Projectile.timeLeft = (int)SwingTime;
            Projectile.extraUpdates = (int)ExtraUpdates;
        }

        public override void AI()
        {
            Timer++;
            if (Timer == 1)
            {
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    Projectile.oldPos[i] = Projectile.position;
                }
            }

            float swingProgress = Timer / SwingTime;
            float easedSwingProgress = Easing.OutExpo(swingProgress, 5f);
            float targetRotation = Projectile.velocity.ToRotation();

            int dir = (int)Dir;

            float xOffset;
            float yOffset;
            if (dir == 1)
            {
                xOffset = SwingXRadius * MathF.Sin(easedSwingProgress * SwingRange + SwingRange);
                yOffset = SwingYRadius * MathF.Cos(easedSwingProgress * SwingRange + SwingRange);
            }
            else
            {
                xOffset = SwingXRadius * MathF.Sin((1f - easedSwingProgress) * SwingRange + SwingRange);
                yOffset = SwingYRadius * MathF.Cos((1f - easedSwingProgress) * SwingRange + SwingRange);
            }


            Projectile.Center = Owner.Center + new Vector2(xOffset, yOffset).RotatedBy(targetRotation);
            Projectile.rotation = (Projectile.Center - Owner.Center).ToRotation() + MathHelper.PiOver4;

            Owner.heldProj = Projectile.whoAmI;
            Owner.ChangeDir(Projectile.velocity.X < 0 ? -1 : 1);
            Owner.itemRotation = Projectile.rotation * Owner.direction;
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;

            // Set composite arm allows you to set the rotation of the arm and stretch of the front and back arms independently
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f)); // set arm position (90 degree offset since arm starts lowered)
            Vector2 armPosition = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Projectile.rotation - (float)Math.PI / 2); // get position of hand

            armPosition.Y += Owner.gfxOffY;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (!_exploded)
            {
                float num = 8;
                for(float i = 0; i < num; i++)
                {
                    float progress = i / num;
                    float rot = MathHelper.TwoPi * progress;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, rot.ToRotationVector2(), 
                        ModContent.ProjectileType<VulcanBreakerBlowtorchProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }

                // EXPLOSION AND RECOIL
                Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Owner.Center, 1024f, 32f);
                float recoilStrength = 16;
                Vector2 direction = target.DirectionTo(Owner.Center);
                Vector2 targetVelocity = direction * recoilStrength;
                Owner.velocity = VectorHelper.VelocityUpTo(Owner.velocity, targetVelocity);

                SoundStyle soundStyle = SoundRegistry.CombusterBoom;
                soundStyle.PitchVariance = 0.33f;
                SoundEngine.PlaySound(soundStyle, target.Center);
                _exploded = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Tip Trail
            Vector2[] oldPos = new Vector2[Projectile.oldPos.Length];
            for (int i = 0; i < oldPos.Length; i++)
            {
                oldPos[i] = Projectile.oldPos[i];
                oldPos[i] += Projectile.rotation.ToRotationVector2() * Projectile.width / 3f;
            }

            Texture2D tipSlashTexture = ModContent.Request<Texture2D>("LunarVeilLegacy/Effects/Primitives/Trails/TerraTrail").Value;
            if (SwordSlash == null)
            {
                SwordSlash = new TrailRenderer(tipSlashTexture, TrailRenderer.DefaultPass,
                    (p) => new Vector2(75f),
                    (p) => Color.White * (1f - p));
                SwordSlash.drawOffset = Projectile.Size / 2f;
            }

            float[] rotation = new float[Projectile.oldRot.Length];
            for (int i = 0; i < rotation.Length; i++)
            {
                rotation[i] = Projectile.oldRot[i] - MathHelper.ToRadians(45);
            }
            SwordSlash.Draw(oldPos, rotation);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin();

            //Texture
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);

            float drawScale = 1f;
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, drawScale, SpriteEffects.None, 0);
            return false;
        }
    }
}
