using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace LunarVeilLegacy.NPCs.Bosses.Jack
{
    public class JackSpawnEffect : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rune Spawn Effect");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.height = 1;
            Projectile.width = 1;
            Projectile.extraUpdates = 1;
        }

        float alphaCounter = 3.5f;
        public override void AI()
        {
            alphaCounter -= 0.18f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D4 = Request<Texture2D>("LunarVeilLegacy/Effects/Masks/Extra_56").Value;
            Main.spriteBatch.Draw(texture2D4, Projectile.Center - Main.screenPosition, null, new Color((int)(55f * alphaCounter), (int)(55f * alphaCounter), (int)(45f * alphaCounter), 0), Projectile.rotation, new Vector2(171, 51), 0.4f * (alphaCounter + 0.6f), SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D4, Projectile.Center - Main.screenPosition, null, new Color((int)(55f * alphaCounter), (int)(55f * alphaCounter), (int)(45f * alphaCounter), 0), Projectile.rotation, new Vector2(171, 51), 0.4f * (alphaCounter + 0.6f), SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D4, Projectile.Center - Main.screenPosition, null, new Color((int)(55f * alphaCounter), (int)(55f * alphaCounter), (int)(25f * alphaCounter), 0), Projectile.rotation, new Vector2(171, 51), 0.8f * (alphaCounter + 0.6f), SpriteEffects.None, 0f);
            return true;
        }
    }
}