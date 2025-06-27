using LunarVeilLegacy.Tiles.Structures.AlcadizNGovheil;
using LunarVeilLegacy.Tiles.DrakonicManor;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using LunarVeilLegacy.Tiles.Illuria;

namespace LunarVeilLegacy.Items.Placeable.Illurian
{
    public class IlluriaTreeBigI: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("CurtainLeft");
			// Tooltip.SetDefault("Curtain");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<IlluriaTreeBig>());
			Item.value = 150;
			Item.maxStack = 9999;
			Item.width = 38;
			Item.height = 24;
		}
	}
}