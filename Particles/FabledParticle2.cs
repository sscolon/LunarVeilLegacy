﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Terraria;

namespace LunarVeilLegacy.Particles
{
    public class FabledParticle2 : Particle
	{
		public override void SetDefaults()
		{
			width = 34;
			height = 34;
			Scale = 15f;
			timeLeft = 300;
			SpawnAction = Spawn;
		}

		public override void AI()
		{
			scale *= 0.93f;
			rotation += Utils.Clamp(velocity.X * 0f, -ai[0], ai[0]);
			velocity *= 1.01f;


			if (Scale <= 0f)
				active = false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			Texture2D tex = texture;
			float alpha = timeLeft <= 20 ? 1f - 1f / 20f * (20 - timeLeft) : 1f;

			if (alpha < 0f)
				alpha = 0f;

			Color color = Color.Multiply(new(1f, 1f, 1f, 0), alpha);
			spriteBatch.Draw(tex, position - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), color, MathHelper.ToRadians(ai[0]).AngleLerp(MathHelper.ToRadians((ai[0] * 180f)), (120f - timeLeft) / 120f), new Vector2(tex.Width / 2f, tex.Height / 2f), 0.1f * scale, SpriteEffects.None, 0f);
			return false;
		}
		public void Spawn()
		{
			ai[1] = Main.rand.NextFloat(2f, 8f) / 1f;
			ai[2] = Main.rand.Next(0, 2);
			ai[3] = Main.rand.NextFloat(0f, 20f);
			timeLeft = (int)ai[4] > 0 ? (int)ai[4] : timeLeft;
		}
	}
}