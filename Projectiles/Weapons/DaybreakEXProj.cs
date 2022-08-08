using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AdvancedModDLC.Projectiles.Weapons
{
    public class DaybreakEXProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("破晓之光EX");
        }

        public override string Texture => "Terraria/Images/Projectile_636";

        public override void SetDefaults()
        {
            Projectile.damage = 1000;
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Daybreak, 600);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Microsoft.Xna.Framework.Vector2.Zero, ProjectileID.DaybreakExplosion, damage, knockback, Main.player[Main.myPlayer].whoAmI, 0, 0);
        }

        public override void AI()
        {
            NPC target = Utils.Tool.GetClosestNPC(Projectile.Center, false);

            Projectile.velocity = (Projectile.velocity + (target.Center - Projectile.Center) / 2);
        }
    }
}
