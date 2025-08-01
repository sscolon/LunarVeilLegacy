﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Harvesting
{
    internal class JungleRuneI : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.value = Item.sellPrice(silver: 20);
            Item.questItem = true;
            Item.rare = ItemRarityID.Quest;
        }
	}
}
