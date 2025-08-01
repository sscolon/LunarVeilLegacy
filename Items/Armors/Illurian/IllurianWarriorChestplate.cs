﻿using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Armors.Illurian
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class IllurianWarriorChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Celestia Moon Breastplate");
			/* Tooltip.SetDefault("Magical essence of an empress!"
				+ "\n+60 max mana and +1 minion slot" +
				"\n+20 Health"); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 10); // How many coins the item is worth
			Item.rare = ItemRarityID.LightRed; // The rarity of the item
			Item.defense = 27; // The amount of defense the item will give when equipped
		}

	

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}