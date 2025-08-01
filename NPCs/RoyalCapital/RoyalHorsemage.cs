﻿using LunarVeilLegacy.Assets.Biomes;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using LunarVeilLegacy.Items.Accessories;
using LunarVeilLegacy.NPCs.Bosses.INest;
using LunarVeilLegacy.Utilis;
using System;
using Terraria.Audio;
using Terraria.GameContent;

using static Terraria.ModLoader.ModContent;
using System.IO;
using LunarVeilLegacy.Projectiles.Magic;

namespace LunarVeilLegacy.NPCs.RoyalCapital
{
	public class RoyalHorsemage : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Morrowed Swampster");
			Main.npcFrameCount[NPC.type] = 5;
			NPCID.Sets.TrailCacheLength[NPC.type] = 10;
			NPCID.Sets.TrailingMode[NPC.type] = 0;

		}


		public enum ActionState
		{

			Speed,
			Wait
		}
		// Current state
		public int frameTick;
		// Current state's timer
		public float timer;

		// AI counter
		public int counter;

		public ActionState State = ActionState.Wait;
		public override void SetDefaults()
		{
			NPC.width = 114;
			NPC.height = 100;
			NPC.damage = 40;
			NPC.defense = 30;
			NPC.lifeMax = 1300;
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 560f;
			NPC.knockBackResist = .45f;
			NPC.aiStyle = 26;
			AIType = NPCID.Unicorn;
			NPC.noTileCollide = false;
			NPC.noGravity = false;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome<AlcadziaBiome>())
			{

				return 0.6f;

			}

			//Else, the example bone merchant will not spawn if the above conditions are not met.
			return 0f;
		}

		int invisibilityTimer;
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 11; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SilverCoin, 1, -1f, 1, default, .61f);
			}


		}
		private int attackCounter;
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.18f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attackCounter = reader.ReadInt32();
		}
		public float ty = 0;
		
		public override void AI()
		{

			timer++;
			NPC.spriteDirection = NPC.direction;

			invisibilityTimer++;
			if (invisibilityTimer >= 100)
			{



				invisibilityTimer = 0;
			}
			NPC.noTileCollide = false;


			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (attackCounter > 0)
				{
					attackCounter--; // tick down the attack counter.
				}

				Player target = Main.player[NPC.target];

				
				// If the attack counter is 0, this NPC is less than 12.5 tiles away from its target, and has a path to the target unobstructed by blocks, summon a projectile.
				if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 300 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
				{
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

					if (StellaMultiplayer.IsHost)
                    {
                        int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 3,
                        ProjectileID.DD2SquireSonicBoom, 60, 0, Main.myPlayer);
                        Main.projectile[projectile].timeLeft = 300;
                        Projectile ichor = Main.projectile[projectile];
                        ichor.hostile = true;
                        ichor.friendly = false;
						NetMessage.SendData(MessageID.SyncProjectile);


                    }

                    attackCounter = 300;
					NPC.netUpdate = true;
				}
			}
		}

		public virtual string GlowTexturePath => Texture + "_Glow";
		private Asset<Texture2D> _glowTexture;
		public Texture2D GlowTexture => (_glowTexture ??= (ModContent.RequestIfExists<Texture2D>(GlowTexturePath, out var asset) ? asset : null))?.Value;
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
		{
			Lighting.AddLight(NPC.Center, Color.GreenYellow.ToVector3() * 1.25f * Main.essScale);
			SpriteEffects Effects = ((base.NPC.spriteDirection != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			Vector2 center = NPC.Center + new Vector2(0f, NPC.height * -0.1f);

			// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
			Vector2 direction = Main.rand.NextVector2CircularEdge(NPC.width * 0.6f, NPC.height * 0.6f);
			float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
			Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

			Vector2 frameOrigin = NPC.frame.Size();
			Vector2 offset = new Vector2(NPC.width - frameOrigin.X + 5, NPC.height - NPC.frame.Height + 3);
			Vector2 DrawPos = NPC.position - screenPos + frameOrigin + offset;

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

				spriteBatch.Draw(texture, DrawPos + new Vector2(0f, 2).RotatedBy(radians) * time, NPC.frame, new Color(53, 10, 112, 0), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, DrawPos + new Vector2(0f, 4).RotatedBy(radians) * time, NPC.frame, new Color(152, 2, 255, 0), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			var drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Width() * 0.5f, NPC.height * 0.5f);
			for (int k = 0; k < NPC.oldPos.Length; k++)
			{
				Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + NPC.Size / 2 + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(Color.Lerp(new Color(190, 50, 250), new Color(72, 13, 255), 1f / NPC.oldPos.Length * k) * (1f - 1f / NPC.oldPos.Length * k));
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, Effects, 0f);
			}

			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			return true;
		}


		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlcaricMush>(), 2, 1, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarianWood>(), 1, 1, 5));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WickofSorcery>(), 900, 1, 1));


		}




		public void Speed()
		{
			timer++;


			if (timer > 50)
			{

				NPC.velocity.X *= 5f;
				NPC.velocity.Y *= 0.5f;
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin, NPC.direction, -1f, 1, default, .61f);

					if (StellaMultiplayer.IsHost)
					{
                        float speedXB = NPC.velocity.X * Main.rand.NextFloat(-0.5f, 0.5f);
                        float speedY = NPC.velocity.Y * Main.rand.Next(0, 0) * 0.0f + Main.rand.Next(-4, 4) * 0f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X, NPC.position.Y, speedXB * 3, speedY,
                            ProjectileID.GreekFire3, 25, 0f, Main.myPlayer);
                    }
				
				}





			}

			if (timer == 100)
			{
				State = ActionState.Wait;
				timer = 0;
			}

		}
	}
}