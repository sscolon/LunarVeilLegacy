﻿
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Assets.Biomes
{
    public class CatacombWater : ModBiome
    {

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Catacombs");

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.Find<ModUndergroundBackgroundStyle>("LunarVeilLegacy/SeaTempleBackgroundStyle");
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => MapBackground;
        public override Color? BackgroundColor => base.BackgroundColor;


        public override bool IsBiomeActive(Player player) => BiomeTileCounts.InCatawater;
        public override void OnEnter(Player player) => player.GetModPlayer<MyPlayer>().ZoneCatacombsWater = true;
        public override void OnLeave(Player player) => player.GetModPlayer<MyPlayer>().ZoneCatacombsWater = false;
    }
}