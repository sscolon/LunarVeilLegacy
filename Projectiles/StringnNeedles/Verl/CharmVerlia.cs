﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Buffs.Charms;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace LunarVeilLegacy.Projectiles.StringnNeedles.Verl
{
    public class CharmVerlia : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charm Spragald");
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.damage = 0;
			Projectile.width = 1;
			Projectile.height = 1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.timeLeft = 600;
			Projectile.scale = 0.9f;
			DrawOriginOffsetX = -110;
			DrawOriginOffsetY = -100;
		}

		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public override void AI()
		{
			Timer++;
			Player player = Main.player[Projectile.owner];
			if (player.noItems || player.CCed || player.dead || !player.active)
				Projectile.Kill();

			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, true);
			float swordRotation = 0f;
			if (Main.myPlayer == Projectile.owner)
			{
				player.ChangeDir(Projectile.direction);
				swordRotation = (Main.MouseWorld - player.Center).ToRotation();
			}

			Projectile.velocity = swordRotation.ToRotationVector2();
			if (Timer == 1)
			{
				player.AddBuff(ModContent.BuffType<VerliaBroo>(), 600);
			}
			if (!Projectile.active)
			{
				player.ClearBuff(ModContent.BuffType<VerliaBroo>());
			}

			Projectile.Center = playerCenter + Projectile.velocity * 1f;// customization of the hitbox position			
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 14)
				{
					Projectile.frame = 0;
				}
			}
		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			overPlayers.Add(index);
		}
	}
}