﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Terraria;
using static Terraria.ModLoader.ModContent;


namespace LunarVeilLegacy.Particles
{
    public class HeatwaveTrailParticle2 : Particle
	{
		public override void SetDefaults()
		{
			width = 34;
			height = 34;
			Scale = 1.5f;
			timeLeft = 40;
			SpawnAction = Spawn;
		}
		public override void AI()
		{
			

			scale *= 1.04f;

			rotation += Utils.Clamp(velocity.X * 2f, -ai[0], ai[0]);

		
			

			if (Scale <= 0f)
				active = false;

			if (Scale >= 10f)
				active = false;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			Texture2D tex = texture;
			float alpha = timeLeft <= 20 ? 1f - 1f / 20f * (20 - timeLeft) : 1f;

			if (alpha < 0f)
				alpha = 0f;

			Color color = Color.Multiply(new(3f, 3f, 3f, 0), alpha);
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