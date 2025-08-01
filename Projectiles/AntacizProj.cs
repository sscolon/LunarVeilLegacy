﻿using Microsoft.Xna.Framework;
using ParticleLibrary;
using LunarVeilLegacy.Particles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles
{
    public class AntacizProj : ModProjectile
    {
        public byte Timer;
        public override void SetDefaults()
        {
            Projectile.damage = 15;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.height = 80;
            Projectile.width = 80;
            Projectile.friendly = true;
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        public override void OnSpawn(IEntitySource source)
        {
            ParticleManager.NewParticle(Projectile.Center, Projectile.velocity * 0, ParticleManager.NewInstance<Spinew>(), Color.Purple, 0.4f, Projectile.whoAmI);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float rotation = Projectile.rotation;
            player.RotatedRelativePoint(Projectile.Center);
            Projectile.rotation -= 0.5f;

            if (Main.mouseLeft && Main.myPlayer == Projectile.owner)
            {
                Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * Projectile.Distance(Main.MouseWorld) / 12;
                Projectile.netUpdate = true;
            }
            else
            {
                Projectile.velocity = Projectile.DirectionTo(player.Center) * 20;
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                }
            }

            Vector3 RGB = new(2.55f, 2.55f, 0.94f);
            // The multiplication here wasn't doing anything
            Lighting.AddLight(Projectile.Center, RGB.X, RGB.Y, RGB.Z);

            player.heldProj = Projectile.whoAmI;
            player.ChangeDir(Projectile.velocity.X < 0 ? -1 : 1);
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = rotation * player.direction;
            //Projectile.netUpdate = true;
        }
    }
}

