using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MoreSumAcc.Item
{
    internal class SmallShield : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Small cute little shield for your minion, not to protect themselves\"" +
                "\nIncrease 5 defenses when you are close to your Minion");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<ProtectingPlayer>().SmallShield = true;
            if (player.GetModPlayer<ProtectingPlayer>().IsClose)
            {
                player.statDefense += 5;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood,10)
                .AddRecipeGroup("3rd tier Bar",5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class ProtectingPlayer : ModPlayer
    {
        public bool SmallShield;
        public bool IsClose;
        public override void PostUpdate()
        {
            if (SmallShield)
            {
                float Distance = 275;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].minion && Player.whoAmI == Main.projectile[i].owner)
                    {
                        float PlayerDisToMinion = Vector2.Distance(Player.Center, Main.projectile[i].Center);
                        if (PlayerDisToMinion < Distance)
                        {
                            IsClose = true;
                        }
                        else
                        {
                            IsClose = false;
                        }
                    }
                }
            }
        }

        public override void ResetEffects()
        {
            SmallShield = false;
        }
    }
}
