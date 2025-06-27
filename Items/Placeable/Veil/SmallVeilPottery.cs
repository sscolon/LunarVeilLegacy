using LunarVeilLegacy.Tiles.RoyalCapital;
using LunarVeilLegacy.Tiles.DrakonicManor;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using LunarVeilLegacy.Tiles.Veil;
using LunarVeilLegacy.Tiles.Fable;

namespace LunarVeilLegacy.Items.Placeable.Veil
{
	public class SmallVeilPottery : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("CurtainLeft");
			// Tooltip.SetDefault("Curtain");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<SmallVeilPot>());
			Item.value = 150;
			Item.maxStack = 9999;
			Item.width = 38;
			Item.height = 24;
		}
	}
}