﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Projectiles;
using LunarVeilLegacy.Projectiles.Slashers;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Melee
{
    public class Auroran : ClassSwapItem
    {
        public int dir;
        public override DamageClass AlternateClass => DamageClass.Ranged;

        public override void SetClassSwappedDefaults()
        {
            Item.damage = 15;
     
        }
        public int AttackCounter = 1;
        public int combowombo = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Auroran"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("Sends shockwaves through the air" +
                "\nHitting enemies with sword will increase speed!" +
                "\nDivergency Inspired!"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 0;
            Item.height = 0;
            Item.useTime = 100;
            Item.useAnimation = 100;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.noMelee = true;

            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AuroranSwordProj>();
            Item.shootSpeed = 20f;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.GetModPlayer<MyPlayer>().SwordCombo >= 0)
            {
                type = ModContent.ProjectileType<AuroranSwordProj>();

            }
            if (player.GetModPlayer<MyPlayer>().SwordCombo >= 4)
            {
                type = ModContent.ProjectileType<AuroranSwordProj2>();
                SoundEngine.PlaySound(SoundID.Item34, player.position);
            }
        }

         public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
        
            int dir = AttackCounter;
            if (player.direction == 1)
            {
                player.GetModPlayer<CorrectSwing>().SwingChange = AttackCounter;
            }
            else
            {
                player.GetModPlayer<CorrectSwing>().SwingChange = AttackCounter * -1;

            }
            AttackCounter = -AttackCounter;
            SoundStyle soundStyle = SoundRegistry.IceyWind;
            soundStyle.PitchVariance = 0.33f;
            SoundEngine.PlaySound(soundStyle, position);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1, dir);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AuroranBullet>(), damage * 2, knockback, player.whoAmI, 1, dir);
            Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<AuroranBullet2>(), damage * 2, knockback, player.whoAmI, 1, dir);
            Projectile.NewProjectile(source, position, velocity * 1.2f, ModContent.ProjectileType<AuroranBullet3>(), damage * 2, knockback, player.whoAmI, 1, dir);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ModContent.ItemType<RippedFabric>(), 3);
            recipe.AddIngredient(ModContent.ItemType<FrileBar>(), 22);
            recipe.AddIngredient(ItemID.FallenStar, 9);
            recipe.Register();
        }
    }
}