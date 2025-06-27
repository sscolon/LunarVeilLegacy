﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles
{
    public class VeriplantWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			
			Main.wallDungeon[Type] = true;

			DustType = ModContent.DustType<Solution>();
			RegisterItemDrop(ModContent.ItemType<Items.Materials.VeriplantGrassWall>());

			AddMapEntry(new Color(69, 85, 37));
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}