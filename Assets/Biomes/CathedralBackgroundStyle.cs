﻿using Terraria.ModLoader;

namespace LunarVeilLegacy.Backgrounds
{
    public class CathedralBackgroundStyle : ModSurfaceBackgroundStyle
	{
		// Use this to keep far Backgrounds like the mountains.
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}

		public override int ChooseFarTexture()
		{
			return BackgroundTextureLoader.GetBackgroundSlot("LunarVeilLegacy/Assets/Textures/Backgrounds/IceyBiomeBackground");
			
		}


		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			
			scale = 0.6f;
			parallax = 0.3;
			b = 1200;
			return BackgroundTextureLoader.GetBackgroundSlot("LunarVeilLegacy/Assets/Textures/Backgrounds/Cathedralflat");

			
		}

        
	}
}
