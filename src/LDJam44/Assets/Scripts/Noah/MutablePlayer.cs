namespace Assets.Scripts.Noah
{
    public class MutablePlayer
    {
        public int LifeForce;
        public ProductState[] Products;
        public int[] Counts;
        public string StationName;

        public int Thrusters;
        public int Stabilizers;
        public int Trading;
        public int Looting;
        public int Drones;
        public int Amp;
        public int Shields;
        public int Drain;

        public MutablePlayer(PlayerState player)
        {
            LifeForce = player.LifeForce;
            Products = new ProductState[0];
            Counts = new[] {0, 0, 0};
            StationName = player.StationName;
            Thrusters = player.Thrusters;
            Stabilizers = player.Stabilizers;
            Trading = player.Trading;
            Looting = player.Looting;
            Drones = player.Drones;
            Amp = player.Amp;
            Shields = player.Shields;
            Drain = player.Drain;
        }
    }
}
