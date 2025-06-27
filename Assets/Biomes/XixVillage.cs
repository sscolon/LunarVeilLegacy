﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Assets.Biomes
{
	// Shows setting up two basic biomes. For a more complicated example, please request.
	public class XixVillage : ModBiome
	{
		public bool IsPrimaryBiome = true; // Allows this biome to impact NPC prices


		// Select all the scenery
		 // Sets a water style for when inside this biome


		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("LunarVeilLegacy/StarbloomBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;



		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Witchtown4");
		/*
		public override void SpecialVisuals(Player player, bool isActive)
		{

			if (!SkyManager.Instance["LunarVeilLegacy:VillageSky"].IsActive() && isActive)
				SkyManager.Instance.Activate("LunarVeilLegacy:VillageSky", player.Center);
			if (SkyManager.Instance["LunarVeilLegacy:VillageSky"].IsActive() && !isActive)
				SkyManager.Instance.Deactivate("LunarVeilLegacy:VillageSky");

		}
		*/
		// Populate the Bestiary Filter

		public override bool IsBiomeActive(Player player) => BiomeTileCounts.InXixVillage;

		public override string BestiaryIcon => base.BestiaryIcon;
		public override string BackgroundPath => base.BackgroundPath;
		public override Color? BackgroundColor => base.BackgroundColor;

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cathedral of the Moon");
		}


		public override void OnEnter(Player player) => player.GetModPlayer<MyPlayer>().ZoneVillage = true;
		public override void OnLeave(Player player) => player.GetModPlayer<MyPlayer>().ZoneVillage = false;
		// Calculate when the biome is active.


	}
}
