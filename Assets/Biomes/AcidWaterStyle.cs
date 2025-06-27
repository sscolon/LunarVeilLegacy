﻿
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;


namespace LunarVeilLegacy.Assets.Biomes
{
    public class AcidWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("LunarVeilLegacy/AcidWaterfallStyle").Slot;
        public override int GetSplashDust() => DustID.DungeonGreen;
        public override int GetDropletGore() => GoreID.WaterDripJungle;
        public override Color BiomeHairColor() => Color.Green;

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }
    }
}