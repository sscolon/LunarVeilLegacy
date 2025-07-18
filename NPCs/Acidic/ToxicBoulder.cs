
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Buffs;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Utilis;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LunarVeilLegacy.NPCs.Acidic
{

    public class ToxicBoulder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toxic Boulder");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Tumbleweed];
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust && !Main.pumpkinMoon && !Main.snowMoon))
            {
                return spawnInfo.Player.ZoneAcid() ? 0.7f : 0f;
            }
            return 0f;
        }

        public override void SetDefaults()
        {
            NPC.width = 35;
            NPC.height = 35;
            NPC.damage = 23;
            NPC.defense = 6;
            NPC.lifeMax = 110;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath4;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 26;
            Main.npcFrameCount[NPC.type] = 1;
            AIType = NPCID.Tumbleweed;  //npc behavior
            AnimationType = NPCID.Tumbleweed;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            if (Main.hardMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GraftedSoul>(), 2, 1, 5));
            }
            npcLoot.Add(ItemDropRule.Common(ItemType<VirulentPlating>(), minimumDropped: 1, maximumDropped: 4));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height,
                    ModContent.DustType<Dusts.GlowDust>(), newColor: ColorFunctions.AcidFlame);
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height,
                    ModContent.DustType<Dusts.GunFlash>(), newColor: ColorFunctions.AcidFlame);
                Main.dust[d].rotation = (Main.dust[d].position - NPC.position).ToRotation() - MathHelper.PiOver4;
            }

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CursedTorch, 0f, -2f, 0, default(Color), .8f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    Main.dust[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    if (Main.dust[num].position != NPC.Center)
                        Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 6f;
                }
            }
        }

        public override void AI()
        {
            if (Main.rand.NextBool(6))
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height,
                    ModContent.DustType<Dusts.GunFlash>(), newColor: ColorFunctions.AcidFlame);
                Main.dust[d].rotation = (Main.dust[d].position - NPC.position).ToRotation() - MathHelper.PiOver4;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Lighting.AddLight(NPC.Center, Color.GreenYellow.ToVector3() * 1.25f * Main.essScale);
            SpriteEffects Effects = ((base.NPC.spriteDirection != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            var drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Width() * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + NPC.Size / 2 + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(Color.Lerp(new Color(255, 253, 90), new Color(72, 131, 56), 1f / NPC.oldPos.Length * k) * (1f - 1f / NPC.oldPos.Length * k));
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, Effects, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<AcidFlame>(), 200);
        }
    }
}