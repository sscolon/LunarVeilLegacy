﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Consumables;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles.Catacombs
{
    //TODO: Smart Cursor Outlines and tModLoader support
    public class CatacombsDoorClosed : LockedDoor
	{
        public override int KeyType => ModContent.ItemType<CatacombsKey>();
		public override Color FailColor => Color.Gold;
		public override string FailString => "Kill wall of flesh! Key needed!";
	}
}
