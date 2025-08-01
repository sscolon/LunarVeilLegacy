﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Projectiles.IgniterExplosions;
using LunarVeilLegacy.UI.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Projectiles.Pikmin
{
    internal class BluePikminAttack : ModProjectile
    {
        private float _lighting;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 19;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 25;

        }
        int Attacktime = 0;
        private bool _setOffset;
        private Vector2 _offset;
        public override void AI()
        {


            int targetNpc = (int)Projectile.ai[0];
            NPC target = Main.npc[targetNpc];
            if (target.active && !_setOffset)
            {
                _offset = (target.position - Projectile.position);
                _setOffset = true;
            }
            else if (!target.active)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity,
                    ModContent.ProjectileType<BluePikminThrow>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
                Projectile.Kill();
            }
            else
            {
                Vector2 targetPos = target.position - _offset;
                Vector2 directionToTarget = Projectile.position.DirectionTo(targetPos);
                float dist = Vector2.Distance(Projectile.position, targetPos);
                Projectile.velocity = (directionToTarget * dist) + new Vector2(0.001f, 0.001f);
            }


            Projectile.rotation = Projectile.velocity.ToRotation();
            Visuals();
            Attacktime++;

            if (Attacktime >= 20)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<NailKaboom>(), Projectile.damage, 0, Projectile.owner);

                Attacktime = 0;

                switch (Main.rand.Next(5))
                {
                    case 0:
                        SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Pikminhit1") with { Volume = 0.4f });
                        break;

                    case 1:
                        SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Pikminhit2") with { Volume = 0.4f });
                        break;

                    case 2:
                        SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Pikminhit3") with { Volume = 0.4f });
                        break;

                    case 3:
                        SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Pikminhit4") with { Volume = 0.4f });
                        break;

                    case 4:
                        SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/Pikminhit5") with { Volume = 0.4f });
                        break;

                }
            }
        }

        private void Visuals()
        {
            DrawHelper.AnimateTopToBottom(Projectile, 3);
            if (Main.rand.NextBool(60))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch);
            }
        }



        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(4f, 4f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.IceTorch, speed * 4);
                d.noGravity = true;
            }
        }
    }
}
