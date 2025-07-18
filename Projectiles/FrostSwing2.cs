﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Buffs;
using LunarVeilLegacy.Dusts;
using LunarVeilLegacy.UI.Systems;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles
{
	public class FrostySwing2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("FrostSwProj");
			Main.projFrames[base.Projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			Projectile.width = 406;
			Projectile.height = 190;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 16;
			Projectile.ignoreWater = true;
		}
		public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			Vector2 angle = new Vector2(Projectile.ai[0], Projectile.ai[1]);
			Projectile.rotation = angle.ToRotation();
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + angle - new Vector2(Projectile.width / 2, Projectile.height / 2);
			if (Projectile.timeLeft == 2)
			{
				Projectile.friendly = false;
			}


			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 50f);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			ShakeModSystem.Shake = 4;
			for (int i = 0; i < 8; i++)
			{
				Dust.NewDustPerfect(target.Center, ModContent.DustType<GlowDust>(), (Vector2.One * Main.rand.Next(1, 3)).RotatedByRandom(19.0), 0, Color.DeepSkyBlue, 0.5f).noGravity = true;
			}
			for (int i = 0; i < 4; i++)
			{
				Dust.NewDustPerfect(target.Center, ModContent.DustType<TSmokeDust>(), (Vector2.One * Main.rand.Next(1, 5)).RotatedByRandom(19.0), 0, Color.AliceBlue, 0.5f).noGravity = true;
			}
			for (int i = 0; i < 8; i++)
			{
				Dust.NewDustPerfect(target.Center, ModContent.DustType<GlowDust>(), (Vector2.One * Main.rand.Next(1, 3)).RotatedByRandom(19.0), 0, Color.Red, 0.5f).noGravity = true;
			}
			base.OnHitNPC(target, hit, damageDone);

		}
	}
}
