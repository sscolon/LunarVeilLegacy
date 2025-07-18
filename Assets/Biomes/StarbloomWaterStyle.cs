﻿
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Assets.Biomes
{
    public class StarbloomWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("LunarVeilLegacy/StarbloomWaterfallStyle").Slot;
        public override int GetSplashDust() => DustID.PinkCrystalShard;
        public override int GetDropletGore() => GoreID.WaterDripHallow;
        public override Color BiomeHairColor() => Color.LightGoldenrodYellow;

       
    }
}