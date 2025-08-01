﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Bosses.Fenix.Projectiles
{
	public class Slashers : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verlia's Swords Dance");
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 90;
			Projectile.scale = 0.8f;
			DrawOffsetX = -175;
			DrawOriginOffsetY = -175;
		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public float Gruh = 0;

		public override void AI()
		{
			Gruh++;

			Vector3 RGB = new(0.89f, 2.53f, 2.55f);
			// The multiplication here wasn't doing anything
			Lighting.AddLight(Projectile.position, RGB.X, RGB.Y, RGB.Z);

			NPC npc = Main.npc[(int)Projectile.ai[1]];
			Projectile.Center = npc.Center;

		
			if (Gruh == 1)
            {

            }

			if (Gruh == 10)
			{
				Gruh = 0;
			}
		}

		public override bool PreAI()
		{
			Projectile.tileCollide = false;




			return true;


		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(200, 200, 200, 0) * (1f - Projectile.alpha / 50f);
		}


	}

}