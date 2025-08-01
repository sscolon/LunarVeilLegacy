﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Bosses.Fenix.Projectiles
{
	public class FenixBlade5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verlia's Swords Dance");
			Main.projFrames[Projectile.type] = 60;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.width = 413 / 2;
			Projectile.height = 131 / 2;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120;
			Projectile.scale = 2f;

		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void AI()
		{
			Projectile.rotation -= 0.05f;
			Vector3 RGB = new(0.89f, 2.53f, 2.55f);
			// The multiplication here wasn't doing anything
			Lighting.AddLight(Projectile.position, RGB.X, RGB.Y, RGB.Z);

			NPC npc = Main.npc[(int)Projectile.ai[1]];
			Projectile.Center = npc.Center;

			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 60)
				{
					Projectile.frame = 0;
				}
			}
			Projectile.alpha += 2;

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