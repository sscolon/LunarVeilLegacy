using LunarVeilLegacy.Tiles;
using System;
using Terraria.ModLoader;

namespace LunarVeilLegacy.UI.Systems
{
    public class FabledBiomeTileCount : ModSystem
	{
		public int BlockCount;

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			BlockCount = tileCounts[ModContent.TileType<GovheilTile>()];
		}
	}
}
