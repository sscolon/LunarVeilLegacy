﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.IgniterExplosions
{
	public class FunBoom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("FrostShotIN");
			Main.projFrames[Projectile.type] = 44;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.width = 194;
			Projectile.height = 166;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 44;
			Projectile.scale = 1f;

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
			if (++Projectile.frameCounter >= 1)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 44)
				{
					Projectile.frame = 0;
				}
			}
			return true;


		}


		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			behindNPCs.Add(index);
			behindProjectiles.Add(index);

		}
	}

}