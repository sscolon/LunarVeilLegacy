
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Consumables;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles.Ishtar
{
	//TODO: Smart Cursor Outlines and tModLoader support
	public class IshtarDoorClosed : LockedDoor
	{
		public override int KeyType => ModContent.ItemType<IshtarKey>();
		public override string FailString => "The door requires an Ishtar Key.";
		public override Color FailColor => Color.LightSkyBlue;
	}
}
