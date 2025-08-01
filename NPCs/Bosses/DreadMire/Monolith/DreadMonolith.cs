﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using ReLogic.Content;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.NPCs.Bosses.DreadMire.Heart;
using LunarVeilLegacy.Particles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;



namespace LunarVeilLegacy.NPCs.Bosses.DreadMire.Monolith
{
    [AutoloadBossHead]
    public class DreadMonolith : ModNPC
    {
        private ref float Timer => ref NPC.ai[0];
        private const float Immune_Distance = 46 * 9;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 14;
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
        }

        private void OnlyTakeDamageWhenClose()
        {
            NPC.dontTakeDamage = true;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (Vector2.Distance(NPC.Center, player.Center) < Immune_Distance)
                {
                    NPC.dontTakeDamage = false;
                    break;
                }
            }
        }

        public override void AI()
        {
            OnlyTakeDamageWhenClose();
            Timer++;
            //Oscillate movement
            float ySpeed = MathF.Sin(Timer * 0.05f);
            NPC.velocity = new Vector2(0, ySpeed);

            float num = 1f - NPC.alpha / 255f;
            Lighting.AddLight(NPC.Center, 1.6f * num, 0.4f * num, 0.8f * num);
        }

        public override bool CheckActive()
        {
            //Returning false here makes it's not despawn
            return false;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new DreadMonolithPersonProfile();
        }

        public class DreadMonolithPersonProfile : ITownNPCProfile
        {
            public int RollVariation() => 0;
            public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

            public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
            {
                if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                    return ModContent.Request<Texture2D>("LunarVeilLegacy/NPCs/Bosses/DreadMire/Monolith/DreadMonolith");

                if (npc.altTexture == 1)
                    return ModContent.Request<Texture2D>("LunarVeilLegacy/NPCs/Bosses/DreadMire/Monolith/DreadMonolith_Head");

                return ModContent.Request<Texture2D>("LunarVeilLegacy/NPCs/Bosses/DreadMire/Monolith/DreadMonolith");
            }

            public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("LunarVeilLegacy/NPCs/Bosses/DreadMire/Monolith/DreadMonolith_Head");
        }

        public override bool CanChat()
        {
            return false;
        }


        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.2f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.width = 100;
            NPC.height = 108;
            NPC.damage = 1;
            NPC.defense = 19;
            NPC.lifeMax = 200;

            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath23;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
                new FlavorTextBestiaryInfoElement(LangText.Bestiary(this, "A crystal which holds the power of summoning one of the three deities")),
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                var EntitySource = NPC.GetSource_Death();
                Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(NPC.Center, 2048f, 128f);
                SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/BloodCystDeath"));
                var entitySource = NPC.GetSource_FromThis();
                int Gore1 = ModContent.Find<ModGore>("LunarVeilLegacy/DreadMonolith1").Type;
                int Gore2 = ModContent.Find<ModGore>("LunarVeilLegacy/DreadMonolith2").Type;
                int Gore3 = ModContent.Find<ModGore>("LunarVeilLegacy/DreadMonolith3").Type;
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, Gore1);
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, Gore2);
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, Gore3);
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, Gore2);
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, Gore3);
            }
            else
            {
                for (int k = 0; k < 7; k++)
                {

                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.Purple, 0.3f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, default, .34f);
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DreadFragment>()));
        }

        public override void OnKill()
        {
            base.OnKill();
            int monolithNumber = (int)NPC.ai[1];
            switch (monolithNumber)
            {
                case 0:
                    NPC.SetEventFlagCleared(ref DownedBossSystem.downedDreadMonolith1, -1);
                    break;
                case 1:
                    NPC.SetEventFlagCleared(ref DownedBossSystem.downedDreadMonolith2, -1);
                    break;
                case 2:
                    NPC.SetEventFlagCleared(ref DownedBossSystem.downedDreadMonolith3, -1);
                    break;
            }
            NetMessage.SendData(MessageID.WorldData);
        }

        Vector2 Drawoffset => new Vector2(0, NPC.gfxOffY) + Vector2.UnitX * NPC.spriteDirection * 0;
        public virtual string GlowTexturePath => Texture + "_Glow";
        private Asset<Texture2D> _glowTexture;
        public Texture2D GlowTexture => (_glowTexture ??= (RequestIfExists<Texture2D>(GlowTexturePath, out var asset) ? asset : null))?.Value;
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float num108 = 4;
            float num107 = (float)Math.Cos((double)(Main.GlobalTimeWrappedHourly % 1.4f / 1.4f * 6.28318548f)) / 2f + 0.5f;
            float num106 = 0f;
            Color color1 = Color.Red * num107 * .8f;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(
                GlowTexture,
                NPC.Center - Main.screenPosition + Drawoffset,
                NPC.frame,
                color1,
                NPC.rotation,
                NPC.frame.Size() / 2,
                NPC.scale,
                effects,
                0
            );
            SpriteEffects spriteEffects3 = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 vector33 = new Vector2(NPC.Center.X, NPC.Center.Y) - Main.screenPosition + Drawoffset - NPC.velocity;
            Color color29 = new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Color.DarkRed);
            for (int num103 = 0; num103 < 4; num103++)
            {
                Color color28 = color29;
                color28 = NPC.GetAlpha(color28);
                color28 *= 1f - num107;
                Vector2 vector29 = NPC.Center + (num103 / (float)num108 * 6.28318548f + NPC.rotation + num106).ToRotationVector2() * (4f * num107 + 2f) - Main.screenPosition + Drawoffset - NPC.velocity * num103;
                Main.spriteBatch.Draw(GlowTexture, vector29, NPC.frame, color28, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, spriteEffects3, 0f);
            }

            Color color2 = Lighting.GetColor((int)(NPC.position.X + NPC.frame.Width * 0.5) / 16, (int)((NPC.position.Y + NPC.frame.Height * 0.5) / 16.0));
            Vector2 drawOrigin = new Vector2(NPC.frame.Width * 0.5f, NPC.frame.Height * 0.5f);
            int r1 = color2.R;
            drawOrigin.Y += 34f;
            drawOrigin.Y += 8f;
            --drawOrigin.X;
            Vector2 position1 = NPC.Center - Main.screenPosition;
            Texture2D texture2D2 = TextureAssets.GlowMask[239].Value;
            float num11 = (float)(Main.GlobalTimeWrappedHourly % 1.0 / 1.0);
            float num12 = num11;
            if (num12 > 0.5)
                num12 = 1f - num11;
            if (num12 < 0.0)
                num12 = 0.0f;
            float num13 = (float)((num11 + 0.5) % 1.0);
            float num14 = num13;
            if (num14 > 0.5)
                num14 = 1f - num13;
            if (num14 < 0.0)
                num14 = 0.0f;
            Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
            drawOrigin = r2.Size() / 2f;
            Vector2 position3 = position1 + new Vector2(6, 5);
            Color color3 = new Color(255, 8, 55) * 1.6f;

            float num15 = 1f + num11 * 0.75f;
            Main.spriteBatch.Draw(texture2D2, position3, r2, color3 * num12, NPC.rotation, drawOrigin, NPC.scale * 1.43f * num15, SpriteEffects.FlipHorizontally, 0.0f);
            float num16 = 1f + num13 * 0.75f;
            Main.spriteBatch.Draw(texture2D2, position3, r2, color3 * num14, NPC.rotation, drawOrigin, NPC.scale * 1.43f * num16, SpriteEffects.FlipHorizontally, 0.0f);

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Vector2 center = NPC.Center + new Vector2(0f, NPC.frame.Height * -0.1f);
            Lighting.AddLight(NPC.Center, Color.DarkRed.ToVector3() * 0.45f * Main.essScale);
            // This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
            Vector2 direction = Main.rand.NextVector2CircularEdge(NPC.frame.Width * 0.6f, NPC.frame.Height * 0.6f);
            float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
            Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;



            Vector2 frameOrigin = NPC.frame.Size();
            Vector2 offset = new Vector2(NPC.frame.Width - frameOrigin.X + 0, NPC.frame.Height - NPC.frame.Height + 5);
            Vector2 drawPos = NPC.position - screenPos + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Main.GlobalTimeWrappedHourly / 3f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;
            SpriteEffects Effects = NPC.spriteDirection != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer / 2) * MathHelper.TwoPi;

                spriteBatch.Draw(GlowTexture, drawPos + new Vector2(0f, 16).RotatedBy(radians) * -time, NPC.frame, new Color(99, 39, 51, 10), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer / 2) * MathHelper.TwoPi;

                spriteBatch.Draw(GlowTexture, drawPos + new Vector2(0f, 8 * 2).RotatedBy(radians) * time, NPC.frame, new Color(59, 8, 21, 20), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
            }
            return true;

        }
    }
}