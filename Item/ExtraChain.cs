using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Item
{
    internal class ExtraChain : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"It is longer but much heavier\"" +
                "\nIncrease whip range by 75% but decrease whip speed by 25%");
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
            player.whipRangeMultiplier += 0.75f;
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) -= 0.25f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Chain, 20)
                .AddIngredient(ItemID.Vine, 5)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }
    }
}
