public class MutableTravelPlan
{
    public int Distance;
    public int Difficulty;
    public string Destination;

    public MutableTravelPlan(TravelPlanState travelPlan)
    {
        Distance = travelPlan.Distance;
        Difficulty = travelPlan.Difficulty;
        Destination = travelPlan.Destination;
    }
}
