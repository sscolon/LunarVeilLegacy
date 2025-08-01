﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Swords
{
    internal class HeatBombRayLight : ModNPC
    {
        public bool Down;
        public float Rot;
        public bool Lightning;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sun Stalker Lighting");
        }

        public virtual string GlowTexturePath => Texture + "_Glow";
        private Asset<Texture2D> _glowTexture;
        public Texture2D GlowTexture => (_glowTexture ??= (ModContent.RequestIfExists<Texture2D>(GlowTexturePath, out var asset) ? asset : null))?.Value;
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (GlowTexture is not null)
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                Vector2 halfSize = new Vector2(GlowTexture.Width / 2, GlowTexture.Height / Main.npcFrameCount[NPC.type] / 2);
                spriteBatch.Draw(
                    GlowTexture,
                    new Vector2(NPC.position.X - screenPos.X + (NPC.width / 2) - GlowTexture.Width * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + NPC.height - GlowTexture.Height * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + halfSize.Y * NPC.scale + Main.NPCAddHeight(NPC) + NPC.gfxOffY),
                    NPC.frame,
                    Color.White,
                    NPC.rotation,
                    halfSize,
                    NPC.scale,
                    spriteEffects,
                0);
            }
        }

        public override void SetDefaults()
        {
            NPC.alpha = 255;
            NPC.width = 0;
            NPC.height = 0;
            NPC.damage = 8;
            NPC.defense = 8;
            NPC.lifeMax = 156;
            NPC.value = 30f;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.dontTakeDamage = true;
            NPC.dontCountMe = true;
            NPC.friendly = true;
        }
        float alphaCounter = 0;
        float counter = 5;


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Texture2D texture2D4 = ModContent.Request<Texture2D>("LunarVeilLegacy/Effects/Masks/RayLight").Value;
            Main.spriteBatch.Draw(texture2D4, (NPC.Center - Main.screenPosition), null, new Color((int)(75f * alphaCounter), (int)(75f * alphaCounter), (int)(65f * alphaCounter), 0), NPC.rotation, new Vector2(171 / 2, 51 / 2), 0.2f * (counter + 0.3f), SpriteEffects.None, 0f);
            return true;
        }
        public override void AI()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Dust dust = Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.CopperCoin);
                dust.velocity *= -1f;
                dust.scale *= .8f;
                dust.noGravity = true;

                Vector2 vector2_1 = new Vector2(Main.rand.Next(-180, 181), Main.rand.Next(-180, 181));
                vector2_1.Normalize();
                Vector2 vector2_2 = vector2_1 * (Main.rand.Next(50, 200) * 0.04f);
                dust.velocity = vector2_2;
                vector2_2.Normalize();
                Vector2 vector2_3 = vector2_2 * 34f;
                dust.position = NPC.Center - vector2_3;
                NPC.netUpdate = true;
            }
            counter -= 0.05f;
            if (!Down)
            { 
                alphaCounter += 0.4f;
                if(alphaCounter >= 5)
                {
                    Down = true;

                }
            }
            else
            {
                if (alphaCounter <= 0)
                {
                    NPC.active = false;

                }
                alphaCounter -= 0.14f;
            }

            if (!Lightning)
            {
                Rot = Main.rand.NextFloat(-0.05f, 0.05f);
                Lightning = true;
                NPC.rotation = Main.rand.NextFloat(360);
            }
            NPC.rotation -= Rot;
        }
    }
}

