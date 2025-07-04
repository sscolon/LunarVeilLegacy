﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles
{
    public class SkullShotKaboom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Boom");
			Main.projFrames[Projectile.type] = 42;
		}
		
		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 42;
			Projectile.scale = 1.3f;
			
		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(100, 38, 22, 0) * (1f - Projectile.alpha / 50f);
		}
		public override void AI()
        {
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			Main.dust[dust].noGravity = true;

			Vector3 RGB = new(2.55f, 2.55f, 0.94f);
			// The multiplication here wasn't doing anything
			Lighting.AddLight(Projectile.position, RGB.X, RGB.Y, RGB.Z);
		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			overPlayers.Add(index);
			overWiresUI.Add(index);
		}
		public override bool PreAI()
		{
			Projectile.tileCollide = false;
			if (++Projectile.frameCounter >= 1)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 42)
				{
					Projectile.frame = 0;
				}
			}
			return true;

			
		}
	
	}
}