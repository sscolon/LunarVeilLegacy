using LunarVeilLegacy.Projectiles.Powders;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using LunarVeilLegacy.Items.Harvesting;
using LunarVeilLegacy.Items.Materials;
using LunarVeilLegacy.Items.Ores;
using LunarVeilLegacy.Items.Weapons.Thrown;
using LunarVeilLegacy.Projectiles.Nails;
using LunarVeilLegacy.Projectiles.Paint;


namespace LunarVeilLegacy.Items.Weapons.PowdersItem
{
    internal class AbyssalPowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sepsis Powder");
			/* Tooltip.SetDefault("Throw magical dust on them!" +
				"\nA sparkly star dust that does double damage as the igniter!"); */
		}

		//+ player.GetModPlayer<MyPlayer>().PPPaintDMG2;
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<VoidPowderProj>();
			Item.autoReuse = true;
			Item.shootSpeed = 12f;
			Item.crit = 2;
			Item.UseSound = SoundID.Grass;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity *= player.GetModPlayer<MyPlayer>().IgniterVelocity, type, damage, knockback, player.whoAmI);
			return false;
		}

	}
}