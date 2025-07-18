﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Assets.Biomes;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Weapons.Mage;
using LunarVeilLegacy.Utilis;
using LunarVeilLegacy.WorldG;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.NPCs.Ishtar
{
    // These three class showcase usage of the WormHead, WormBody and WormTail classes from Worm.cs
    internal class CentipedeHead : WormHead
    {
        private ref float Timer => ref NPC.ai[0];
        private ref float CollideTimer => ref NPC.ai[1];
        private Player Target => Main.player[NPC.target];

        public override int BodyType => ModContent.NPCType<CentipedeBody>();

        public override int TailType => ModContent.NPCType<CentipedeTail>();

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            NPC.CloneDefaults(NPCID.DiggerHead);
            //If your worm is not connected, messed with this NPC.width number
            //It is based on that, lower will make them closer, higher will make them farther
            NPC.width = 24;
            NPC.height = 58;
            NPC.damage = 90;
            NPC.defense = 10;
            NPC.lifeMax = 3000;
            NPC.value = 5000f;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 1;
            NPC.noGravity = false;
            NPC.aiStyle = -1;
            AIType = 0;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit29;
            NPC.DeathSound = SoundID.NPCDeath32;
            NPC.noTileCollide = false;
        }

        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 36;
            MaxSegmentLength = 42;

            CommonWormInit(this);
        }

        // This method is invoked from ExampleWormHead, ExampleWormBody and ExampleWormTail
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 13f;
            worm.Acceleration = 0.1f;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override bool? CanFallThroughPlatforms()
        {
            return NPC.HasValidTarget && Target.position.Y > NPC.position.Y + 32;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            if (NPC.HasValidTarget)
            {
                float xDir = 0;
                float yDir = 0;
                if (Target.position.X < NPC.position.X)
                {
                    xDir = -1;
                }
                else
                {
                    xDir = 1;
                }

                if (NPC.collideX)
                {
                    CollideTimer++;
                    if (CollideTimer >= 15)
                    {
                        NPC.noTileCollide = true;
                        CollideTimer = 0;
                    }
                }
                else
                {
                    CollideTimer = 0;
                }

                if (NPC.noTileCollide)
                {
                    yDir = -2;
                    CollideTimer++;
                    if (CollideTimer >= 10)
                    {
                        NPC.noTileCollide = false;
                        CollideTimer = 0;
                    }
                }

                float moveSpeed = 2f;
                float distanceToTarget = Vector2.Distance(NPC.Center, Target.Center);

                Vector2 velocity = new Vector2(xDir * moveSpeed, NPC.velocity.Y + yDir);
                if (distanceToTarget <= 32f)
                {
                    velocity.X *= (distanceToTarget / 32f);
                }

                Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

                NPC.velocity.X = velocity.X;
                NPC.velocity.Y = velocity.Y;
                //  NPC.rotation = NPC.velocity.ToRotation();
            }
            else
            {
                NPC.velocity.X *= 0.99f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnRates.GetIshtarEnemySpawnChance(spawnInfo);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RibbonStaff>(), 5, 1, 1));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float rotation = NPC.rotation;
            rotation -= MathHelper.PiOver2;
            if (rotation.ToRotationVector2().X < 0)
            {
                NPC.spriteDirection = 1;
            }
            else if (rotation.ToRotationVector2().X > 0)
            {
                NPC.spriteDirection = -1;
            }


            Lighting.AddLight(screenPos, Color.Green.ToVector3() * 0.66f * Main.essScale);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    }

    internal class CentipedeBody : WormBody
    {
        private static float _last;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            //If your worm is not connected, messed with this NPC.width number
            //It is based on that, lower will make them closer, higher will make them farther
            NPC.width = 20;
            NPC.height = 24;
            NPC.defense = 150;
            NPC.aiStyle = -1;
            AIType = 0;
        }


        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.33f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void Init()
        {
            CentipedeHead.CommonWormInit(this);
            NPC.frameCounter += _last;
            _last++;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float rotation = NPC.rotation;
            rotation -= MathHelper.PiOver2;
            if (rotation.ToRotationVector2().X < 0)
            {
                NPC.spriteDirection = 1;
            }
            else if (rotation.ToRotationVector2().X > 0)
            {
                NPC.spriteDirection = -1;
            }

            Lighting.AddLight(screenPos, Color.Green.ToVector3() * 0.66f * Main.essScale);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    }

    internal class CentipedeTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            //If your worm is not connected, messed with this NPC.width number
            //It is based on that, lower will make them closer, higher will make them farther
            NPC.width = 24;
            NPC.height = 58;
            NPC.aiStyle = -1;
            AIType = 0;
        }

        public override void Init()
        {
            CentipedeHead.CommonWormInit(this);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float rotation = NPC.rotation;
            rotation -= MathHelper.PiOver2;
            if (rotation.ToRotationVector2().X < 0)
            {
                NPC.spriteDirection = 1;
            }
            else if (rotation.ToRotationVector2().X > 0)
            {
                NPC.spriteDirection = -1;
            }

            Lighting.AddLight(screenPos, Color.Green.ToVector3() * 0.66f * Main.essScale);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    }
}