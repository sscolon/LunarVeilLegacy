﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Materials
{
    internal class Starrdew : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Starr Dew");
			/* Tooltip.SetDefault("Ew! Its sticky! I wonder what else is sticky..." +
			"\nA sticky substance that resonates with the stars and the morrow!"); */
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(silver: 5);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
