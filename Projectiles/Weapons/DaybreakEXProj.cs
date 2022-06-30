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

        public override void AI()
        {
            NPC target = Utils.Tool.GetClosestNPC(Projectile.Center, false);

            
        }
    }
}
