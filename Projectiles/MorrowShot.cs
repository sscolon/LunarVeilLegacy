﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles
{
    public class MorrowShot : ModProjectile
    {
        public float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("morrowshot");
			Main.projFrames[Projectile.type] = 1;
			//The recording mode
		}

		public override void SetDefaults()
		{
			Projectile.damage = 12;
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.light = 1.5f;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.maxPenetrate = 3;
			Projectile.ownerHitCheck = true;
		}

		public override void AI()
		{
			Timer++;
			float distance = 0f;
			Player player = Main.player[Projectile.owner];
			
			if (Timer <= 1)
				Projectile.position = player.position + Vector2.UnitY.RotatedBy(MathHelper.ToRadians(Projectile.ai[1])) * distance;
			if (Timer < 2)
				Projectile.velocity = 20 * Vector2.Normalize(Projectile.DirectionTo(Main.MouseWorld));

			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			
			if (Projectile.velocity.Y > 16f)
				Projectile.velocity.Y = 16f;
			
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
				Projectile.rotation += MathHelper.Pi;

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			float speedX = Projectile.velocity.X * Main.rand.NextFloat(.2f, .3f) + Main.rand.NextFloat(-4f, 4f);
			float speedY = Projectile.velocity.Y * Main.rand.Next(20, 35) * 0.01f + Main.rand.Next(-10, 11) * 0.2f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.Spark, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
			Projectile.Kill();
		}

		public override bool PreDraw(ref Color lightColor)
		{
			int height = Main.player[Projectile.owner].height / 1; // 5 is frame count
			int y = height * Projectile.frame;
			var rect = new Rectangle(0, y, Main.player[Projectile.owner].width, height);
			var drawOrigin = new Vector2(Main.player[Projectile.owner].width / 2, Projectile.height / 2);

			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
	}
}
