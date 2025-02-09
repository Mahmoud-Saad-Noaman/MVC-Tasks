namespace Dependency_injection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new PetrolEngine(); // Dependency is created

            var car = new Car(engine); // Dependency is injected
            car.StartCar();
        }
    }

    public interface IEngine
    {
        void Start();
    }

    public class PetrolEngine : IEngine
    {
        public void Start()
        {
            Console.WriteLine("Petrol Engine Started.");
        }
    }

    public class Car
    {
        private readonly IEngine _engine;

        // Dependency is injected using ctor constructor
        public Car(IEngine engine)
        {
            _engine = engine;
        }

        public void StartCar()
        {
            _engine.Start();
            Console.WriteLine("Car is running.");
        }
    }
}
