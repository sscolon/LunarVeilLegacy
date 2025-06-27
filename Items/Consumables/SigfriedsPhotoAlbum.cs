

using LunarVeilLegacy.Helpers;
using LunarVeilLegacy.Items.Weapons.Mage;
using LunarVeilLegacy.Items.Weapons.Melee.Greatswords;
using LunarVeilLegacy.Items.Weapons.Melee.Greatswords.INY;
using LunarVeilLegacy.Items.Weapons.Ranged.GunSwapping;
using LunarVeilLegacy.Items.Weapons.Summon;
using LunarVeilLegacy.Items.Weapons.Summon.Orbs;
using LunarVeilLegacy.Items.Weapons.Thrown.Jugglers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Items.Consumables
{
    public class SigfriedsPhotoAlbum : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electronic Death Remote (EDR)");
            // Tooltip.SetDefault("'that big red button probably will do something you’ll regret... \n Your conscience advises you to press it and see what happens!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 18;
            Item.height = 28;
            Item.rare = ModContent.RarityType<GoldenSpecialRarity>();
            Item.value = Item.sellPrice(0, 0, 1, 0);
     
        }


       
    }
}