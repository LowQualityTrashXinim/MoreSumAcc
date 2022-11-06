using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Items
{
    internal class FocusLens : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"For nerd\"" +
            "\nYour whip when tagged a enemy will give them a debuff that allow your minion to do mini Crit" +
            "\nDebuff give 5% to do mini crit" +
            "\nMini crit deal extra 35%");
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
            player.GetModPlayer<NerdWhipPlayer>().WhipBuffNerd = true;
        }
    }
    public class NerdWhipPlayer : ModPlayer
    {
        public bool WhipBuffNerd;
        public override void ResetEffects()
        {
            WhipBuffNerd = false;
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if(proj.DamageType == DamageClass.SummonMeleeSpeed)
            {
                target.AddBuff(ModContent.BuffType<MiniCrit>(), 90);
            }
        }
    }
    public class MiniCrit : ModBuff
    {
        public override string Texture => "MoreSumAcc/EmptyBuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GetWhippedByNerd>().MiniCritAllow = true;
        }
    }

    public class GetWhippedByNerd : GlobalNPC
    {
        public bool MiniCritAllow;

        public override bool InstancePerEntity => true;
        public override void ResetEffects(NPC npc)
        {
            MiniCritAllow = false;
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.DamageType == DamageClass.Summon && MiniCritAllow && Main.rand.NextBool(20))
            {
                damage += (int)(damage * .35f);
            }
        }
    }
}
