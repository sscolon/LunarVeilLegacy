﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Projectiles.Crossbows.Sniper;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Weapons.Ranged.Crossbows
{

    public class JadeJungleCrossbow : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wooden Crossbow"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("Use a small crossbow and shoot three bolts!"
                + "\n'Triple Threat!'"); */
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 25;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
            Item.shootSpeed = 40f;
            Item.shoot = ModContent.ProjectileType<JadeJungleCrossbowHold>();
            Item.scale = 0.8f;
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(silver: 70);
            Item.noUseGraphic = true;
            Item.channel = true;


        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ModContent.ItemType<BlankCrossbow>(), 1);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddIngredient(ItemID.WoodenBow, 1);
            recipe.Register();

        }




    }
}