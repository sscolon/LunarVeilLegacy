﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ParticleLibrary;
using LunarVeilLegacy.Dusts;
using LunarVeilLegacy.Particles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Trails;
using LunarVeilLegacy.Utilis;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using LunarVeilLegacy.UI.Systems;

namespace LunarVeilLegacy.NPCs.Bosses.Fenix.Projectiles
{
	public class CrileShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SalfaCirle");
			Main.projFrames[Projectile.type] = 20;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 35;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}
		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.width = 75;
			Projectile.height = 75;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.knockBack = 12.9f;
			Projectile.aiStyle = 1;
			AIType = ProjectileID.Bullet;
			Projectile.tileCollide = false;
			Projectile.hostile = true;

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

		public float nigga = 0;
		public override void AI()
		{

			nigga++;

			Projectile.velocity *= 1.02f;
			if (nigga < 2)
			{
				ShakeModSystem.Shake = 10;
			}


			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 20)
				{
					Projectile.frame = 0;
				}
			}


		}

		public PrimDrawer TrailDrawer { get; private set; } = null;
		public float WidthFunction(float completionRatio)
		{
			float baseWidth = Projectile.scale * (130 / 4) * 1.3f;
			return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
		}
		public Color ColorFunction(float completionRatio)
		{
			return Color.Lerp(Color.White, Color.Transparent, completionRatio) * 0.7f;
		}
		public override bool PreDraw(ref Color lightColor)
		{


			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			TrailDrawer ??= new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["VampKnives:BasicTrail"]);
			GameShaders.Misc["VampKnives:BasicTrail"].SetShaderTexture(TrailRegistry.SmallWhispyTrail);
			TrailDrawer.DrawPrims(Projectile.oldPos, Projectile.Size * 0.5f - Main.screenPosition, 155);
			return true;
		}
	}
}



