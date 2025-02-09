namespace Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Usage
            var house = new HouseBuilder()
                .BuildWalls("Brick Walls")
                .BuildRoof("Tile Roof")
                .InstallWindows("Glass Windows")
                .InstallDoors("Wooden Doors")
                .Build();
        }
    }

    public class House
    {
        public string Walls { get; set; }
        public string Roof { get; set; }
        public string Windows { get; set; }
        public string Doors { get; set; }
    }

    public class HouseBuilder
    {
        private House _house = new House();

        public HouseBuilder BuildWalls(string walls)
        {
            _house.Walls = walls;
            return this;
        }

        public HouseBuilder BuildRoof(string roof)
        {
            _house.Roof = roof;
            return this;
        }

        public HouseBuilder InstallWindows(string windows)
        {
            _house.Windows = windows;
            return this;
        }

        public HouseBuilder InstallDoors(string doors)
        {
            _house.Doors = doors;
            return this;
        }

        public House Build()
        {
            return _house;
        }
    }

}