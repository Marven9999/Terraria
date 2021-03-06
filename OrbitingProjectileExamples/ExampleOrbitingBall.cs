using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Trinitarian.Projectiles;

namespace Trinitarian.Projectiles.OrbitingProjectileExamples
{
    public class ExampleOrbitingBall : OrbitingProjectile
    {
        public override void SetDefaults()
        {
            projectile.ignoreWater = false;
            projectile.width = 30;
            projectile.penetrate = 1;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.light = 1f;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.timeLeft = 60 * 60 * 3;
            ProjectileSlot = 1;
            Period = 300;
            PeriodFast = 100;
            ProjectileSpeed = 8;
            OrbitingRadius = 300;
            CurrentOrbitingRadius = 300;
        }
        public override string Texture => "Trinitarian/Projectiles/Mage/OrbitingBall";
        public override void AI()
        {
            player = Main.player[projectile.owner];
            RelativeVelocity = player.velocity;
            OrbitCenter = player.Center;
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            if (Proj_State == 1 || Proj_State == 2)
            {
                GeneratePositionsAfterKill();
            }
        }
        public override void Attack()
        {
            Vector2 ProjectileVelocity = Main.MouseWorld - projectile.Center;
            if (ProjectileVelocity != Vector2.Zero)
            {
                ProjectileVelocity.Normalize();
            }
            ProjectileVelocity *= 16;
            projectile.velocity = ProjectileVelocity;
            Proj_State = 5;
            //This method is responsible for correctly reordering the projetiles when one of them dies. We call this here to make sure the already fired projectiles do not count towards the current ones.
            GeneratePositionsAfterKill();
        }
    }
}