﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Projectiles.IgniterExplosions;
using LunarVeilLegacy.UI.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Accessories.Catacombs
{
    internal class FireEmblemPlayer : ModPlayer
    {
        public bool hasFireEmblem;
        public int fireEmblemCooldown;
        public override void ResetEffects()
        {
            hasFireEmblem = false;
        }

        public override void PostUpdateEquips()
        {
            if (fireEmblemCooldown > 0)
                fireEmblemCooldown--;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hasFireEmblem && fireEmblemCooldown <= 0)
            {
                switch (Main.rand.Next(0, 4))
                {
                    case 0:
                        target.AddBuff(BuffID.OnFire3, 120);
                        break;
                    case 1:
                        target.AddBuff(BuffID.ShadowFlame, 120);
                        break;
                    case 2:
                        target.AddBuff(BuffID.CursedInferno, 120);
                        break;
                    case 3:
                        target.AddBuff(BuffID.Daybreak, 60);
                        break;
                }

                if (hit.Crit && Main.rand.NextBool(2))
                {
                    ShakeModSystem.Shake = 10;
                    SoundStyle soundStyle = new SoundStyle($"LunarVeilLegacy/Assets/Sounds/Kaboom");
                    soundStyle.PitchVariance = 0.15f;
                    SoundEngine.PlaySound(soundStyle, target.position);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero,
                        ModContent.ProjectileType<FireBoom>(), damageDone / 2, hit.Knockback, Player.whoAmI);
                }

                fireEmblemCooldown = 120;
            }
        }
    }

    internal class FireEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FireEmblemPlayer>().hasFireEmblem = true;
        }
    }
}
