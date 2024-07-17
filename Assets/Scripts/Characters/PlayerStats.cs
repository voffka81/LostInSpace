using System.Collections.Generic;


public class PlayerStats
{
    public static Dictionary<StatsId, object> CreateInitialStats()
    {
        return new Dictionary<StatsId, object>()
        {
            {StatsId.Money, new NumericStat("Money", 100.0f,10000000f)},
            {StatsId.RentAccount, new NumericStat("Rent Account", 0,10f)},
            {StatsId.Food, new NumericStat("Food Energy", 50f,100f) },
            {StatsId.Energy,new NumericStat("Energy", 50f,100f) },
            {StatsId.LocationName,new StringStat("Location","Nowhere") },
        };
    }



    //// Knowledge for University Jobs
    //public Stat literatureKnowledge = new Stat("LiteratureKnowledge", 0);
    //public Stat mathematicsKnowledge = new Stat("MathematicsKnowledge", 0);
    //public Stat computerScienceKnowledge = new Stat("ComputerScienceKnowledge", 0);

    //// Knowledge for Factory Jobs
    //public Stat electronicsKnowledge = new Stat("ElectronicsKnowledge", 0);
    //public Stat roboticsKnowledge = new Stat("RoboticsKnowledge", 0);
    //public Stat industrialDesignKnowledge = new Stat("IndustrialDesignKnowledge", 0);

    //public Stat careerPoints = new Stat("careerPoints", 0);

    //// Achievements - to WIN the game
    //public Stat whealthAchievement = new Stat("whealthAchievement", 999999);
    //public Stat educationAchievement = new Stat("educationAchievement", 999999);
    //public Stat careerAchievement = new Stat("careerAchievement", 999999);
    //public Stat happinessAchievement = new Stat("happinessAchievement", 999999);

    //// Inventory items
    //public Stat freezerItem = new Stat("Freezer Item", 400, 1);
    //public Stat clothesItem = new Stat("Clothes Item", 200, 3);
    //public Stat booksItem = new Stat("Books Item", 150, 2);
    //public Stat tvItem = new Stat("Tv Item", 1500, 1);
    //public Stat laptopItem = new Stat("Laptop Item", 3000, 1);
    // Update is called once per frame

}
