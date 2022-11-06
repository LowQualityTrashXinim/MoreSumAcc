using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Items
{
    internal class TapedGun : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Oh you can't be serious\"" +
                "\nWhip weapons now shoot out a bullet every time it get use");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TapedGunPlayer>().taped = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FlintlockPistol)
                .AddIngredient(ItemID.Cobweb, 100)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
    public class TapedGunPlayer : ModPlayer
    {
        public bool taped;
        public override void ResetEffects()
        {
            taped = false;
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.DamageType == DamageClass.SummonMeleeSpeed && taped)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, velocity*20, ProjectileID.Bullet, 20, 1f, Player.whoAmI);
            }
        }
    }
}
