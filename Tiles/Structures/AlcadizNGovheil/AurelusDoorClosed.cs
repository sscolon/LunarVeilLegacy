﻿
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Consumables;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles.Structures.AlcadizNGovheil
{
    //TODO: Smart Cursor Outlines and tModLoader support
    public class GothivDoorClosed : LockedDoor
	{
        public override int KeyType => ModContent.ItemType<GothiviasSeal>();
		public override string FailString => "Hun, you cant open this door yet :(";
        public override Color FailColor => Color.Gold;
	}
}
