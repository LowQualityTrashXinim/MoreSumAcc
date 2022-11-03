using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Item
{
    internal class SummonerCrown : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increase minion slot by 1");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Sapphire, 1)
                .AddIngredient(ItemID.Diamond, 2)
                .AddRecipeGroup("Crowns")
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
