﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.IgniterExplosions
{
	public class KaBoomSpirit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("FrostShotIN");
			Main.projFrames[Projectile.type] = 16;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.width = 192;
			Projectile.height = 192;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 48;
			Projectile.scale = 2f;

		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public override void AI()
		{
			Projectile.rotation -= 0.01f;
			Vector3 RGB = new(0.89f, 2.53f, 2.55f);
			// The multiplication here wasn't doing anything
			Lighting.AddLight(Projectile.position, RGB.X, RGB.Y, RGB.Z);

		}

		public override bool PreAI()
		{
			Projectile.tileCollide = false;
			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 16)
				{
					Projectile.frame = 0;
				}
			}
			return true;


		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 50f);
		}


	}

}