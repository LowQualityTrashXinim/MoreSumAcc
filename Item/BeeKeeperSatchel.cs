using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Item
{
    internal class BeeKeeperSatchel : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Attacks from minions have 10% to inflict poisoned for 5 seconds" +
                "\nIncrease minion slots by 1");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.GetModPlayer<BeeKeeperSatcherPlayer>().BeeKeeper = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 15)
                .AddIngredient(ItemID.BeeWax, 10)
                .AddIngredient(ItemID.Stinger, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class BeeKeeperSatcherPlayer : ModPlayer
    {
        public bool BeeKeeper;
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(proj.DamageType == DamageClass.Summon && BeeKeeper && Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Poisoned, 300);
            }
        }
        public override void ResetEffects()
        {
            BeeKeeper = false;
        }
    }
}
