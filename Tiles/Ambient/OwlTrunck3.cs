﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Harvesting;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace LunarVeilLegacy.Tiles.Ambient
{
    public class OwlTrunck3 : ModTile

    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();

            AddMapEntry(new Color(200, 200, 200), name);
            RegisterItemDrop(ModContent.ItemType<Stick>());
        }
    }
}
