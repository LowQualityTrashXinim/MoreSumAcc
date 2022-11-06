using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreSumAcc.Items
{
    internal class KingSlimeGem : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"The big red crystal that clearly from 1 ruby\"" +
                "\nOn minion hit, minion damage increase by 15% for 5s" +
                "\nHave 6s cooldown");
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
            player.GetModPlayer<KSGemPlayer>().KSGem = true;
        }
    }
    public class KSGemPlayer : ModPlayer
    {
        public bool KSGem = false;
        public override void ResetEffects()
        {
            KSGem = false;
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if(proj.minion && KSGem && !target.HasBuff(ModContent.BuffType<Crystalized>()) && !target.GetGlobalNPC<KSGemNPC>().CDCrystalized)
            {
                target.AddBuff(ModContent.BuffType<Crystalized>(), 300);       
            }
        }
    }
    public class Crystalized : ModBuff
    {
        public override string Texture => "MoreSumAcc/EmptyBuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<KSGemNPC>().Crystalized = true;
            if (npc.buffTime[buffIndex] == 0)
            {
                npc.AddBuff(ModContent.BuffType<CDCrystalized>(), 360);
            }
        }
    }
    public class CDCrystalized : ModBuff
    {
        public override string Texture => "MoreSumAcc/EmptyBuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<KSGemNPC>().CDCrystalized = true;
        }
    }

    public class KSGemNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool Crystalized;
        public bool CDCrystalized;
        public override void ResetEffects(NPC npc)
        {
            Crystalized = false;
            CDCrystalized = false;
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(projectile.minion && Crystalized && !CDCrystalized)
            {
                damage += (int)(damage * .15f);
            }
        }
    }
}
