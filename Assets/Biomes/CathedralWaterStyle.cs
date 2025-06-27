using Microsoft.Xna.Framework;
using LunarVeilLegacy.Dusts;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LunarVeilLegacy.Assets.Biomes
{
    public class CathedralWaterStyle : ModWaterStyle
	{
		public override int ChooseWaterfallStyle() => Find<ModWaterfallStyle>("LunarVeilLegacy/CathedralWaterfallStyle").Slot;
		public override int GetSplashDust() => DustType<Solution>();
		public override int GetDropletGore() => Find<ModGore>("LunarVeilLegacy/EggGore").Type;
		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0f;
			b = 1f;
		}
		public override Color BiomeHairColor()
			=> Color.LightSkyBlue;
	}
}