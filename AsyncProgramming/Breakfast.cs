namespace AsyncProgramming
{
    public class Breakfast
    {
        public static async Task Do1000CoffeeAsync()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(CoffeeAsync());
            }

            Task.WaitAll(tasks.ToArray());
        }


        public static async Task DoBreakfastAsync()
        {
            var coffeeTask = CoffeeAsync();

            await HeatPanAsync();
            var eggsTask = FryEggsAsync();
            var baconTask = FryBaconAsync();

            await ToastBreadAsync();
            var jamTask = JamAsync();

            await JuiceAsync();
            await coffeeTask;
            await eggsTask;
            await baconTask;
            await jamTask;
        }

        public static void DoBreakfast()
        {
            Coffee();
            HeatPan();
            FryEggs();
            FryBacon();
            ToastBread();
            Jam();
            Juice();
        }

        public static async Task CoffeeAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Café listo!");
        }
        public static async Task HeatPanAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Sarten caliente");
        }
        public static async Task FryEggsAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Huevos listos!");
        }
        public static async Task FryBaconAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Tocino listo!");
        }
        public static async Task ToastBreadAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Pan listo!");
        }
        public static async Task JamAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Pan con mermelada listo");
        }
        public static async Task JuiceAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("jugo listo!");
        }

        public static void Coffee()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Café listo!");
        }
        public static void HeatPan()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Sarten caliente");
        }
        public static void FryEggs()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Huevos listos!");
        }
        public static void FryBacon()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Tocino listo!");
        }
        public static void ToastBread()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Pan listo!");
        }
        public static void Jam()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Pan con mermelada listo");
        }
        public static void Juice()
        {
            Thread.Sleep(1000);
            Console.WriteLine("jugo listo!");
        }


    }
}
