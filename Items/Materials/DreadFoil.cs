using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LunarVeilLegacy.Items.Materials
{
    public class DreadFoil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }
    }
}