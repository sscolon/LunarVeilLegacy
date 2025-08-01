﻿using Microsoft.Xna.Framework;
using ParticleLibrary;
using LunarVeilLegacy.Buffs;
using LunarVeilLegacy.Buffs.Dusteffects;
using LunarVeilLegacy.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Powders
{
    public class ArcanePowder : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Powdered Sepsis");
			
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 45;
			Projectile.ignoreWater = true;
		}
        public override void AI()
        {
			for (int j = 0; j < 5; j++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.6f);
				ParticleManager.NewParticle(Projectile.Center, speed * 9, ParticleManager.NewInstance<BurnParticle>(), Color.RosyBrown, Main.rand.NextFloat(0.2f, 0.8f));


			}
			Projectile.velocity *= 0.96f;

        }
        public override bool PreAI()
		{
			Projectile.tileCollide = false;
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f);
			Main.dust[dust].scale = 1f;


			return true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[Projectile.owner];
			player.AddBuff(ModContent.BuffType<UseIgniter>(), 720);
			target.AddBuff(ModContent.BuffType<Dusted>(), 720);
			target.AddBuff(ModContent.BuffType<ArcaneDust>(), 720);
			base.OnHitNPC(target, hit, damageDone);
		}
	}
}