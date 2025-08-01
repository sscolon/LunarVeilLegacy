﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Bosses.Verlia.Projectiles
{
    public class Viola : ModNPC
	{


		public int moveSpeed = 0;
		public int moveSpeedY = 0;
		public int counter;
		public ref float AI_State => ref NPC.ai[0];
	
		public float AiTimer
		{
			get => NPC.ai[0];
			set => NPC.ai[0] = value;
		}
		public ref float AI_FlutterTime => ref NPC.ai[2];

		public int frame = 0;
		public int timer = 0;
		public int timer2 = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viola");
			Main.npcFrameCount[NPC.type] = 1; // make sure to set this for your modNPCs.

			// Specify the debuffs it is immune to
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
		}

		public override void SetDefaults()
		{
			NPC.width = 32; // The width of the NPC's hitbox (in pixels)
			NPC.height = 32; // The height of the NPC's hitbox (in pixels)
			NPC.aiStyle = -1; // This NPC has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.// The amount of damage that this NPC deals // The amount of defense that this NPC has // The amount of health that this NPC has
			NPC.value = 250f; // How many copper coins the NPC will drop when killed.
			NPC.friendly = false;
			NPC.lifeMax = 500;
			NPC.dontTakeDamageFromHostiles = true;
			NPC.noGravity = true;
			NPC.knockBackResist = 0f;
			NPC.damage = 1; // The amount of damage that this NPC deals
			NPC.defense = 0; // The amount of defense that this NPC has
		}
		
		// Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
		public override void AI()
		{
			NPC.velocity *= 0.9f;
			timer2++;

			if (timer2 == 8)
			{
				if (StellaMultiplayer.IsHost)
                {
                    float speedX = NPC.velocity.X * Main.rand.NextFloat(.3f, .3f) + Main.rand.NextFloat(4f, 4f);
                    float speedY = NPC.velocity.Y * Main.rand.Next(-1, -1) * 0.0f + Main.rand.Next(-4, -4) * 0f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + speedX + 10, NPC.position.Y + speedY, speedX * 0, speedY - 2 * 2, 
						ModContent.ProjectileType<Notia>(), 10, 0f, Owner: Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + speedX + 10, NPC.position.Y + speedY, speedX * 0, speedY + 2 * 2, 
						ModContent.ProjectileType<Notia>(), 10, 0f, Owner: Main.myPlayer);
                }
				
				timer2 = 0;
			}


			timer++;
			if (timer == 300)
            {
				NPC.SimpleStrikeNPC(9999, 1, crit: false, 1);
			}




			Vector3 RGB = new(2.55f, 0.45f, 0.94f);
			// The multiplication here wasn't doing anything
			Lighting.AddLight(NPC.position, RGB.X, RGB.Y, RGB.Z);
		}
		
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)






			Vector2 frameOrigin = NPC.frame.Size();
			Vector2 offset = new Vector2(NPC.width - frameOrigin.X, NPC.height - NPC.frame.Height);
			Vector2 drawPos = NPC.position - screenPos + frameOrigin + offset;

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

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, NPC.frame, new Color(90, 70, 255, 50), NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, NPC.frame, new Color(140, 120, 255, 77), NPC.rotation, frameOrigin, NPC.scale, SpriteEffects.None, 0);
			}

			// Using a rectangle to crop a texture can be imagined like this:
			// Every rectangle has an X, a Y, a Width, and a Height
			// Our X and Y values are the position on our texture where we start to sample from, using the top left corner as our origin
			// Our Width and Height values specify how big of an area we want to sample starting from X and Y
			
			
			return false;
        }
        
		
	}
}
