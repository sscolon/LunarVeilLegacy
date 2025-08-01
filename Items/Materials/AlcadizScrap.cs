﻿using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Materials
{
    internal class AlcadizScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kaeva Clot");
			/* Tooltip.SetDefault("Flesh-like substance used for many items!" +
			"\nObtained from blood-typed enemies"); */
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(silver: 5);
		}
	}
}
