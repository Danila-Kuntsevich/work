using System.Collections.Generic;

namespace Game_Save.Model
{
    public class Testimony
    {
        public int Id { get; set; }
        public double ColdWater { get; set; } = 0;
        public double Hotwater { get; set; } = 0;
        public double Electricity { get; set; } = 0;
        public double ElectricityDay { get; set; } = 0;
        public double ElectricityNight { get; set; } = 0;

        public double DHWСoolant { get; set; } = 0;

        public double DHWThermalEnergy { get; set; } = 0;

        public string? Month { get; set; }

    }
}