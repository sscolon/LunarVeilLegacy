﻿using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Armors.Appretience
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class AppretiencePants : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Verl Breastplate");
			/* Tooltip.SetDefault("Shines with a blooming moon"
				+ "\n+10% Ranged and Magic Damage!" +
				"\n+12 Penetration" +
				"\n+5 Critical Strike Chance!" +
				"\n+20 Max Life"); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 10); // How many coins the item is worth
			Item.rare = ItemRarityID.Pink; // The rarity of the item
			Item.defense = 12; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Generic) += 20f;
			player.statLifeMax2 += 20;
		}



		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}