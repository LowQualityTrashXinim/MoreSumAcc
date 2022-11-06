using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MoreSumAcc.Items
{
    internal class MasterMindsHeart : ModItem
    {
        public override string Texture => "MoreSumAcc/MissingTexture";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"You really kept this thing? such a foul stench...\"" +
                "\nIncrease minion slot by 1" +
                "\nIncrease sentry slot by 1" +
                "\nIncrease summon damage by 5%" +
                "\nEmit a confusing wave that confuses mobs that get too close to the player");
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
            player.maxTurrets += 1;
            player.GetDamage<SummonDamageClass>() *= 1.05f;
            player.GetModPlayer<MasterMindHeartPlayer>().MasterMindHeart = true;
        }
    }
    public class MasterMindHeartPlayer : ModPlayer
    {
        public bool MasterMindHeart;
        const int LOL = 100;//Timer
        int counter = 280;
        public override void ResetEffects()
        {
            MasterMindHeart = false;
        }
        public override void PostUpdate()
        {
            if(MasterMindHeart)
            {
                float Distance = 100;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    float targetDisToPlayer = Vector2.Distance(Player.Center, npc.Center);
                    if (targetDisToPlayer < Distance)//check if player is close to a near by NPC
                    {
                        counter++;//Increase timer
                        if (counter >= LOL)//If timer met
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<MindWaveVisualProjectile>(), 0, 0, Player.whoAmI);
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<MindWaveProjectile>(), 1, 0, Player.whoAmI);
                            counter = 0;
                        }
                    }
                }
            }
        }
    }
    //WHAT THE FUCK IS THIS ? THIS IS UTTERLY RETARDED
    //Why do i have to create 2 projectiles just for this to work ???
    public class MindWaveProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.hide = true;//Hide projectile cause it is fucked
        }
        public override void AI()
        {
            //Make hitbox larger as time go on
            Projectile.Hitbox = new Rectangle((int)Projectile.position.X - 1, (int)Projectile.position.Y - 1, Projectile.width + 2, Projectile.height + 2);
            //This alpha think act like a timer for Projectile, kinda like Projectile.timeLeft but for alpha
            Projectile.alpha += 5;
            if(Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Confused, 150);
        }
    }

    public class MindWaveVisualProjectile : ModProjectile
    {
        public override string Texture => "MoreSumAcc/Item/MindWaveProjectile";
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.scale += .1f;//Scale the projectile
            Projectile.alpha += 5;
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}
