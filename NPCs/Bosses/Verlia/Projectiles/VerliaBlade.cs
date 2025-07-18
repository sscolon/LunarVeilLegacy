﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Bosses.Verlia.Projectiles
{
    public class VerliaBlade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verlia's Moon blade");
			Main.projFrames[Projectile.type] = 1;
			//The recording mode
		}
		public override void SetDefaults()
		{
			Projectile.damage = 100;
			Projectile.width = 72;
			Projectile.height = 72;
			Projectile.light = 1.5f;
			Projectile.friendly = false;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 500;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
	
		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public float Timer2;

		public override void AI()
		{

			Timer++;
			Timer2++;
			Projectile.velocity *= 1.05f;

			float speedXabc = -Projectile.velocity.X * Main.rand.NextFloat(0f, 0f) + Main.rand.NextFloat(0f, 0f);
			float speedYabc = -Projectile.velocity.Y * Main.rand.Next(0, 0) * 0.00f + Main.rand.Next(0, 0) * 0.0f;


			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedXabc + 36, Projectile.position.Y + speedYabc + 36, speedXabc * 0, speedYabc * 0, ModContent.ProjectileType<MoonBladeTrail>(), Projectile.damage * 0, 0f, Projectile.owner, 0f, 0f);
			
			


			float maxDetectRadius = 1f; // The maximum radius at which a projectile can detect a target
			float projSpeed = 6f; // The speed at which the projectile moves towards the target

			

			if (Timer2 < 150)
			{
				maxDetectRadius = 2000f;

			}

			
			if (Timer < 6)
			{



				maxDetectRadius = 2f;


			}
			if (Timer > 6 && Timer < 12)
			{



				maxDetectRadius = 2000f;


			}

			if (Timer > 12 && Timer < 18)
			{



				maxDetectRadius = 0f;


			}

			if (Timer > 18 && Timer < 40)
			{



				maxDetectRadius = 2000f;


			}
			if (Timer > 40 && Timer < 68)
			{



				maxDetectRadius = 0f;


			}
			if (Timer > 68 && Timer < 72)
			{



				maxDetectRadius = 2000f;


			}
			if (Timer > 72 && Timer < 90)
			{



				maxDetectRadius = 0f;


			}

			if (Timer > 90 && Timer < 100)
			{



				maxDetectRadius = 2000f;


			}
			if (Timer > 100 && Timer < 140)
			{



				maxDetectRadius = 0f;


			}
			if (Timer > 140 && Timer < 170)
			{



				maxDetectRadius = 2000f;


			}
			if (Timer > 170 && Timer < 190)
			{



				maxDetectRadius = 0f;


			}

			if (Timer == 191)
			{



				Timer = 0;


			}
			// Trying to find NPC closest to the projectile
			Player closestplayer = FindClosestNPC(maxDetectRadius);
			if (closestplayer == null)
				return;



			Projectile.rotation = (Projectile.position - closestplayer.Center).ToRotation() + MathHelper.PiOver2;
			
			
			// If found, change the velocity of the projectile and turn it in the direction of the target
			// Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
			Projectile.velocity = (closestplayer.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;

		}
		
		// Finding the closest NPC to attack within maxDetectDistance range
		// If not found then returns null
		public Player FindClosestNPC(float maxDetectDistance)
		{
			Player closestplayer = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxPlayers; k++)
			{
				Player target = Main.player[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)

				// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
				float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

				// Check if it is within the radius
				if (sqrDistanceToTarget < sqrMaxDetectDistance)
				{
					sqrMaxDetectDistance = sqrDistanceToTarget;
					closestplayer = target;
				}

			}


			return closestplayer;
		}

		
		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 center = Projectile.Center + new Vector2(0f, Projectile.height * -0.1f);

			// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
			Vector2 direction = Main.rand.NextVector2CircularEdge(Projectile.width * 0.6f, Projectile.height * 0.6f);
			float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
			Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)


			


			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
			Vector2 frameOrigin = frame.Size() / 2;
			Vector2 offset = new Vector2(Projectile.width - frameOrigin.X);
			Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Main.GlobalTimeWrappedHourly / 2f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				Main.EntitySpriteDraw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time - new Vector2(60, 60), frame, new Color(220, 70, 255, 80), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				Main.EntitySpriteDraw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time - new Vector2(60, 60), frame, new Color(96, 190, 70, 77), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

	}
}
