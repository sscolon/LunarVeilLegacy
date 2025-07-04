﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Materials.Tech;
using LunarVeilLegacy.Projectiles;
using LunarVeilLegacy.Projectiles.Bow;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Ranged
{
    internal class YuriFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 3, 1, 29);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 30f;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.channel = true;
            Item.scale = 1f;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ArchariliteEnergyShot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Cinderscrap>(), 50);
            recipe.AddIngredient(ModContent.ItemType<MoltenScrap>(), 10);
            recipe.AddIngredient(ModContent.ItemType<MetallicOmniSource>(), 10);
            recipe.AddIngredient(ModContent.ItemType<VeroshotBow>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int Sound = Main.rand.Next(1, 3);
            if (Sound == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/ArchariliteEnergyShot"));
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle("LunarVeilLegacy/Assets/Sounds/ArchariliteEnergyShot2"));
            }

         
                Item.useTime = 25;
                Item.shootSpeed = 30f;
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HeatedShot>(), damage * 2, knockback, player.whoAmI);
                float numberProjectiles = 4;
                float rotation = MathHelper.ToRadians(15);
                position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f; // This defines the projectile roatation and speed. .4f == projectile speed
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.DD2PhoenixBowShot, damage, knockback, player.whoAmI);
                }
            

            return false;
        }



    }
}
