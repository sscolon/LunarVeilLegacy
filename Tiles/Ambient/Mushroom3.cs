﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace LunarVeilLegacy.Tiles.Ambient
{
	public class Mushroom3 : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileMerge[Type][TileID.ClayBlock] = true;
			Main.tileMerge[Type][TileID.Stone] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			RegisterItemDrop(ModContent.ItemType<Items.Harvesting.Mushroom>());
			AddMapEntry(new Color(200, 100, 100), name);
		}


	}
}