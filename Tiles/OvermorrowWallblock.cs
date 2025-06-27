
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Tiles
{
    public class OvermorrowWallblock : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;

			DustType = ModContent.DustType<Solution>();
			RegisterItemDrop(ModContent.ItemType<Items.Materials.OvermorrowWall>());

			AddMapEntry(new Color(200, 200, 200));
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}