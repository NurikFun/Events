using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_v2
{

    public class TemperatureEventArgs : EventArgs
    {
        public int Value { get; set; }
    }

    public interface IEventTrigger
    {
        event EventHandler<TemperatureEventArgs> OnTemperatureChanged;
        int Temperature { set; }
    }

    public class Thermometer : IEventTrigger
    {
        private int maxTemperature;

        public Thermometer(int maxTemperature)
        {
            this.maxTemperature = maxTemperature;
        }

        public event EventHandler<TemperatureEventArgs> OnTemperatureChanged = delegate { };
        public int Temperature {

            set
            {
                var temperature = value;

                if (temperature > maxTemperature)
                {
                    var temperatureValue = new TemperatureEventArgs
                    {
                        Value = temperature
                    };

                    OnTemperatureChanged(this, temperatureValue);
                }

            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            //IEventTrigger trigger = new Mobile(30);

            IEventTrigger trigger = new Thermometer(30);

            trigger.OnTemperatureChanged += (o, e) =>
            {
                Console.WriteLine($"Temperature is changed to {e.Value} °C. {(!(o is Thermometer) ? "Triggered manually by mobile." : "Triggered automatically by the thermometer.")}");
            };

            trigger.OnTemperatureChanged += (o, e) => Console.WriteLine("1. Ordering mint chocolate chip ice-cream!");
            trigger.OnTemperatureChanged += (o, e) => Console.WriteLine("2. Turning on the A/C.");
            trigger.OnTemperatureChanged += (o, e) => Console.WriteLine("3. Closing all doors & windows.");
            
            trigger.Temperature = 32; //when the temperature is below 30 then none of the actions will be performed.

            trigger.Temperature = 33;

            // https://medium.com/@dinesh.jethoe/events-in-c-explained-4a464b110fdc

            Console.ReadKey();
        }
    }
}
