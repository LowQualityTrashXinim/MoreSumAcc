using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MoreSumAcc.Items
{
    internal class CharmOfStar : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Look at that, make a wish will you\"" +
                "\nEach time your minion hit or shot the target" +
                "\nIncrease 1% chance of self spawning a fallen star onto your foe only if it is night time" +
                "\nReset back to 0 if you successfully self spawn one");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            tooltips.Add(new TooltipLine(Mod, "CharmOfStar", "Your chance to spawn a star is (only workable in night) :" + player.GetModPlayer<CharmStarPlayer>().ChanceToActivate + "%"));
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
            player.GetModPlayer<CharmStarPlayer>().CharmOfStar = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Starfury)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class CharmStarPlayer : ModPlayer
    {
        public int ChanceToActivate = 0;
        public bool CharmOfStar = false;
        public override void ResetEffects()
        {
            CharmOfStar = false;
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (CharmOfStar && !Main.dayTime && proj.DamageType == DamageClass.Summon)
            {
                float Chance = ChanceToActivate * 0.01f;
                if (Main.rand.NextFloat() < Chance)
                {
                    ChanceToActivate = 0;
                    Vector2 newPos = new Vector2(target.Center.X + Main.rand.Next(-100, 100), target.Center.Y - 1000);
                    Vector2 velocityTo = (target.Center - newPos).SafeNormalize(Vector2.UnitX) * 30;
                    int projectile = Projectile.NewProjectile(Player.GetSource_FromThis(), newPos, velocityTo, ProjectileID.Starfury, 40, 1f, Player.whoAmI);
                    Main.projectile[projectile].DamageType = DamageClass.Summon;
                }
                else
                {
                    ChanceToActivate++;
                }
            }
        }
    }
}
