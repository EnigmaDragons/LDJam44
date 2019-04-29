using System;

namespace Assets.Scripts.Noah
{
    public class MutablePlayer
    {
        public int LifeForce;
        public ProductState[] Products;
        public int[] Counts;
        public string StationName;
        public float HealthScalingCost;

        public int Thrusters;
        public int Stabilizers;
        public int Trading;
        public int Looting;
        public int Drones;
        public int Amp;
        public int Shields;
        public int Drain;

        public int Health;

        public MutablePlayer(PlayerState player)
        {
            LifeForce = player.LifeForce;
            Products = new ProductState[0];
            Counts = new[] {0, 0, 0};
            StationName = player.StationName;
            HealthScalingCost = player.HealthScalingCost;
            Thrusters = player.Thrusters;
            Stabilizers = player.Stabilizers;
            Trading = player.Trading;
            Looting = player.Looting;
            Drones = player.Drones;
            Amp = player.Amp;
            Shields = player.Shields;
            Drain = player.Drain;
        }

        public void RecaluclateHealth()
        {
            var health = 0;
            var tempLifeForce = LifeForce;
            var nextHitPointCost = 1;
            while (tempLifeForce > nextHitPointCost)
            {
                health++;
                tempLifeForce -= nextHitPointCost;
                nextHitPointCost = (int)Math.Ceiling(nextHitPointCost * HealthScalingCost);
            }
            health++;
            Health = health;
        }
    }
}
