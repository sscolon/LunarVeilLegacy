﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Crossbows.Magical
{
    public class PiaCircle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SalfaCirle");
			Main.projFrames[Projectile.type] = 19;
		}
		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.width = 256;
			Projectile.height = 256;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			DrawOriginOffsetY = 0;
			Projectile.damage = 0;
			Projectile.timeLeft = 60;
		
		}
		public override bool PreAI()
		{
			
			Projectile.tileCollide = false;
			
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 50f);
		}
		public override void AI()
		{
			
			
			Player player = Main.player[Projectile.owner];
			if (player.noItems || player.CCed || player.dead || !player.active)
				Projectile.Kill();

			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, true);
			float swordRotation = 0f;
			if (Main.myPlayer == Projectile.owner)
			{
				player.ChangeDir(Projectile.direction);
				swordRotation = (Main.MouseWorld - player.Center).ToRotation();
				if (!player.channel)
					Projectile.Kill();
			}
			Projectile.velocity = swordRotation.ToRotationVector2();

			Projectile.spriteDirection = player.direction;
			if (Projectile.spriteDirection == 1)
				Projectile.rotation = Projectile.velocity.ToRotation();
			else
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;

			Projectile.Center = playerCenter + new Vector2(90, 0).RotatedBy(swordRotation);

			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 19)
				{
					Projectile.frame = 0;
				}
			}
		}
	}
}



