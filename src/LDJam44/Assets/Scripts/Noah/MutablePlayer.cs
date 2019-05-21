using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Noah
{
    public class MutablePlayer
    {
        public int LifeForce
        {
            get
            {
                return lifeForce;
            }
            set
            {
                lifeForce = value;
                RecaluclateHealth();
            }
        }

        private int lifeForce = 0;
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
            Products = new ProductState[0];
            Counts = new[] {0, 0, 0};
            StationName = player.StationName;
            HealthScalingCost = player.HealthScalingCost;
            galaxy.Upgrades.ToList().ForEach(x => Upgrades[x.name] = 0);
            LifeForce = player.LifeForce;
        }

        private void RecaluclateHealth()
        {
            var health = 0;
            var tempLifeForce = lifeForce;
            var nextHitPointCost = 1;
            while (tempLifeForce > nextHitPointCost)
            {
                health++;
                tempLifeForce -= nextHitPointCost;
                nextHitPointCost = (int)Math.Ceiling(nextHitPointCost * HealthScalingCost);
            }
            health++;
            Health = health + 40;
        }
    }
}
