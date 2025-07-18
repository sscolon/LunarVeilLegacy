﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles
{
    public class FabledColoredSunray : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("BoomCirle");
		}
		public override void SetDefaults()
		{
			Projectile.width = 360;
			Projectile.height = 360;

			Projectile.timeLeft = 510;
			Projectile.scale = 2f;
			Projectile.alpha = 255;
		}

		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public override bool PreAI()
		{
			Projectile.tileCollide = false;

			return true;
		}
		public override void AI()
		{


			Timer++;

			if (Timer < 255)
			{
				Projectile.alpha--;
			}

			if (Timer > 255)
			{
				Projectile.alpha++;
			}


		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 255f);
		}

		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			overPlayers.Add(index);
			overWiresUI.Add(index);
		}
	}
}