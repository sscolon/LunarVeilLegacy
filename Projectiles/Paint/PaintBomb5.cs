﻿using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Paint
{
    public class PaintBomb5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("FrostShotIN");
			Main.projFrames[Projectile.type] = 27;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.width = 68;
			Projectile.height = 80;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 27;
			Projectile.scale = 1.3f;
		}

		public override bool PreAI()
		{
			Projectile.tileCollide = false;
			if (++Projectile.frameCounter >= 1)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 27)
				{
					Projectile.frame = 0;
				}
			}
			return true;
		}
	}
}