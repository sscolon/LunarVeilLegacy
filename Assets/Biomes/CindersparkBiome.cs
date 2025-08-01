﻿
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace LunarVeilLegacy.Assets.Biomes
{
    public class CindersparkBiome : ModBiome
    {
        //public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.Find<ModUndergroundBackgroundStyle>("SpiritMod/Biomes/SpiritUgBgStyle");
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Cinderspark");
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => MapBackground;
        public override Color? BackgroundColor => base.BackgroundColor;


        public override bool IsBiomeActive(Player player) => BiomeTileCounts.InCinder;
        public override void OnEnter(Player player) => player.GetModPlayer<MyPlayer>().ZoneCinder = true;
        public override void OnLeave(Player player) => player.GetModPlayer<MyPlayer>().ZoneCinder = false;
    }
}