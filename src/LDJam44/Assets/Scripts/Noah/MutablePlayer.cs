using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Noah
{
    public class MutablePlayer
    {
        public int LifeForce;
        public ProductState[] Products;
        public int[] Counts;
        public string StationName;
        public float HealthScalingCost;
        public int Loot;
        public Dictionary<string, int> Upgrades = new Dictionary<string, int>();

        public int Health;

        public int UpgradeLevel(string name) => Upgrades[name];

        public MutablePlayer(PlayerState player, GalaxyState galaxy)
        {
            LifeForce = player.LifeForce;
            Products = new ProductState[0];
            Counts = new[] {0, 0, 0};
            StationName = player.StationName;
            HealthScalingCost = player.HealthScalingCost;
            galaxy.Upgrades.ToList().ForEach(x => Upgrades[x.Name] = 0);
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
