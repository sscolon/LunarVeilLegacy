using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using LunarVeilLegacy.Items.Accessories.Foods;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.WorldG;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LunarVeilLegacy.NPCs.Event.Gintzearmy
{

    public class GintzeWindRider : ModNPC
    {


        public bool Dir;
        public float Timer;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Spirit");
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = 0;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.lifeMax = 50;
            NPC.defense = 4;
            NPC.value = 65f;
            NPC.knockBackResist = 0.55f;
            NPC.width = 30;
            NPC.height = 44;
            NPC.damage = 14;
            NPC.scale = 1.0f;
            NPC.lavaImmune = false;
            NPC.alpha = 0;
            NPC.dontTakeDamage = false;
            NPC.HitSound = new SoundStyle("LunarVeilLegacy/Assets/Sounds/Gintze_Hit") with { PitchVariance = 0.1f };
            NPC.DeathSound = new SoundStyle("LunarVeilLegacy/Assets/Sounds/Gintze_Death") with { PitchVariance = 0.1f };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneOverworldHeight && EventWorld.Gintzing) ? (278f) : 0f;
        }
        int frame = 0;
        public override void FindFrame(int frameHeight)
        {


            bool expertMode = Main.expertMode;
            Player player = Main.player[NPC.target];

            NPC.frameCounter += 0.5f;
            if (NPC.frameCounter >= 5)
            {
                frame++;
                NPC.frameCounter = 0;
            }
            if (frame >= 4)
            {
                frame = 0;
            }
            NPC.frame.Y = frameHeight * frame;

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GintzlMetal>(), 6, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlcadizMetal>(), 6, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bread>(), 10, 1, 3));

        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SilverCoin, 2.5f * hit.HitDirection, -2.5f, 180, default, .6f);
            }
            if (NPC.life <= 0)
            {
                EventWorld.GintzeKills += 1;
                for (int i = 0; i < 20; i++)
                {
                    int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Copper, 0f, -2f, 180, default, .6f);
                    Main.dust[num].noGravity = true;
                    Dust expr_62_cp_0 = Main.dust[num];
                    expr_62_cp_0.position.X = expr_62_cp_0.position.X + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    Dust expr_92_cp_0 = Main.dust[num];
                    expr_92_cp_0.position.Y = expr_92_cp_0.position.Y + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    if (Main.dust[num].position != NPC.Center)
                    {
                        Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 6f;
                    }
                }
            }
        }
        Vector2 targetPos2;
        Vector2 targetPos;
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.rotation = NPC.velocity.X * 0.03f;
            targetPos = player.Center;
            NPC.ai[0]++;
            if (NPC.ai[0] >= 200)
            {
                NPC.ai[0] = 0;
            }
            targetPos2 = Vector2.Lerp(targetPos2, targetPos, 0.02f);
            if (NPC.ai[0] == 151)
            {
                if (NPC.position.X >= player.position.X)
                {
                    Dir = true;
   
                }
                else
                {
                    Dir = false;
         
                }

                NPC.velocity.Y *= 0.94f;
            }
            if (NPC.ai[0] >= 150)
            {
                if (Dir)
                {

                    NPC.velocity.X -= 0.9f;
                }
                else
                {
 
                    NPC.velocity.X += 0.9f;
                }

                NPC.velocity.Y *= 0.94f;
            }
            else
            {
                if (NPC.position.X >= player.position.X)
                {
                    Movement(targetPos, 300, 0, 0.006f);
                }
                else
                {
                    Movement(targetPos, -300, 0, 0.006f);
                }
            }


        }
        public void Movement(Vector2 Player2, float PosX, float PosY, float Speed)
        {
            Player player = Main.player[NPC.target];
            Vector2 target = Player2 + new Vector2(PosX, PosY);
            base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, VectorHelper.MovemontVelocity(base.NPC.Center, Vector2.Lerp(base.NPC.Center, target, 0.5f), base.NPC.Center.Distance(target) * Speed), 0.1f);
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
            Color color1 = Color.LightBlue * num107 * .8f;
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
            SpriteEffects spriteEffects3 = (NPC.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 vector33 = new Vector2(NPC.Center.X, NPC.Center.Y) - Main.screenPosition + Drawoffset - NPC.velocity;
            Color color29 = new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Color.White);
            for (int num103 = 0; num103 < 4; num103++)
            {
                Color color28 = color29;
                color28 = NPC.GetAlpha(color28);
                color28 *= 1f - num107;
                Vector2 vector29 = NPC.Center + (num103 / (float)num108 * 6.28318548f + NPC.rotation + num106).ToRotationVector2() * (4f * num107 + 2f) - Main.screenPosition + Drawoffset - NPC.velocity * num103;
                Main.spriteBatch.Draw(GlowTexture, vector29, NPC.frame, color28, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, spriteEffects3, 0f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Lighting.AddLight(NPC.Center, Color.Silver.ToVector3() * 0.25f * Main.essScale);
            SpriteEffects Effects = NPC.spriteDirection != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            var drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Width() * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + NPC.Size / 2 + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(Color.Lerp(new Color(191, 165, 160), new Color(191, 59, 51), 1f / NPC.oldPos.Length * k) * (1f - 1f / NPC.oldPos.Length * k));
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, Effects, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }
    }
}