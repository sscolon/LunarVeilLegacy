﻿using Microsoft.Xna.Framework;
using LunarVeilLegacy.NPCs.Bosses.Niivi;
using LunarVeilLegacy.UI.Systems;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Assets.Biomes
{
	// Shows setting up two basic biomes. For a more complicated example, please request.
	public class IlluriaBiome : ModBiome
	{
		public bool IsPrimaryBiome = true; // Allows this biome to impact NPC prices


		// Select all the scenery
		public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("LunarVeilLegacy/StarbloomWaterStyle"); // Sets a water style for when inside this biome
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("LunarVeilLegacy/StarbloomBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheGreatIlluria");

        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;



        // Populate the Bestiary Filter
        public override string BestiaryIcon => base.BestiaryIcon;
		public override string BackgroundPath => base.BackgroundPath;
		public override Color? BackgroundColor => base.BackgroundColor;

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cathedral of the Moon");
		}

		// Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) =>  BiomeTileCounts.InIlluria || NPC.AnyNPCs(ModContent.NPCType<Niivi>());
		public override void OnEnter(Player player) => player.GetModPlayer<MyPlayer>().ZoneIlluria = true;
		public override void OnLeave(Player player) => player.GetModPlayer<MyPlayer>().ZoneIlluria = false;
	}
}
