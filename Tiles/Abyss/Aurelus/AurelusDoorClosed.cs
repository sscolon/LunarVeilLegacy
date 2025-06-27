
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Consumables;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles.Abyss.Aurelus
{
    //TODO: Smart Cursor Outlines and tModLoader support
    public class AurelusDoorClosed : LockedDoor
	{
		public override int KeyType => ModContent.ItemType<VoidKey>();
		public override string FailString => "You're not ready yet! Key needed!";
		public override Color FailColor => Color.LightSkyBlue;
	}
}
