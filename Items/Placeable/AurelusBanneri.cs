﻿using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Placeable
{
    public class AurelusBanneri : ModItem
	{
        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Abyss.Aurelus.AurelusBanner1>());

            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
