using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Game_Save.Model;
using System.Windows;
using Game_Save.View;
using System.IO;
using System.Windows.Input;

namespace Game_Save.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private WorkDbContext db;

        public double ColdWater { get; set; } = 0;
        public double Hotwater { get; set; } = 0;
        public double Electricity { get; set; } = 0;
        public double ElectricityDay { get; set; } = 0;
        public double ElectricityNight { get; set; } = 0;

        public double DHWСoolant { get; set; } = 0;

        public double DHWThermalEnergy { get; set; } = 0;

        public string? Living { get; set; } = "1";

        private Testimony First;

        public Testimony first
        {
            get => First;
            set
            {
                First = value;
                OnPropertyChanged();
            }
        }

        private double Xvs;

        public double xvs {
            get => Xvs;
            set {
                Xvs = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double GvsCoolant;

        public double gvsCoolant
        {
            get => GvsCoolant;
            set
            {
                GvsCoolant = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double GvsThermalEnergy;

        public double gvsThermalEnergy
        {
            get => GvsThermalEnergy;
            set
            {
                GvsThermalEnergy = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double Gvs;

        public double gvs
        {
            get => Gvs;
            set
            {
                Gvs = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double EeDay;

        public double eeDay
        {
            get => EeDay;
            set
            {
                EeDay = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double EeNight;

        public double eeNight
        {
            get => EeNight;
            set
            {
                EeNight = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private double Ee;

        public double ee
        {
            get => Ee;
            set
            {
                Ee = Math.Round(value, 3);
                OnPropertyChanged();
            }
        }

        private string? Sum;

        public string? sum
        {
            get => Sum;
            set
            {
                Sum = string.Concat(value," р.");
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Testimony> Testimonys { get; set; }

        public double[] rate = new double[] { 35.78 , 158.98 , 4.28, 4.9, 2.31, 35.78, 998.69};
        public double[] standard = new double[] { 4.85 , 4.01, 164, 4.01, 0.05349 };


        public MainWindowVM()
        {
            db = new WorkDbContext();
            db.Database.EnsureCreated();
            db.Testimonys.Load();
            Testimonys = db.Testimonys.Local.ToObservableCollection();
            if (db.Testimonys != null)
            {
                first = db.Testimonys.OrderBy(e => e.Id).LastOrDefault();
            }
        }

        public RelayCommand Add_Click
    => new RelayCommand(obj =>
    {
        var User = new Testimony {
            ColdWater = ColdWater,
            Hotwater = Hotwater,
            Electricity = Electricity,
            ElectricityDay = ElectricityDay,
            ElectricityNight = ElectricityNight,
            DHWСoolant = DHWСoolant,
            DHWThermalEnergy = DHWThermalEnergy
        };
        db.Testimonys.Add(User);
        db.SaveChanges();
        if (first == null)
        {
            first = new Testimony();
            first.ColdWater = 0;
            first.Hotwater = 0;
            first.Electricity = 0;
            first.ElectricityDay = 0;
            first.ElectricityNight = 0;
            first.DHWСoolant = 0;
            first.DHWThermalEnergy = 0;
        }
        xvs = ColdWater != 0 ? (ColdWater - first.ColdWater) * rate[0] : double.Parse(Living) * standard[0] * rate[0];
        gvsCoolant = DHWСoolant != 0 ? (DHWСoolant - first.DHWСoolant) * rate[5] : double.Parse(Living) * standard[3] * rate[5];
        gvsThermalEnergy = DHWThermalEnergy != 0 ? gvsCoolant / rate[5] * 0.05349 * rate[6] : double.Parse(Living) * standard[4] * rate[6];
        gvs = Hotwater != 0 ? (Hotwater - first.Hotwater) * rate[1] : double.Parse(Living) * standard[1] * rate[1];
        eeDay = ElectricityDay != 0 ? (ElectricityDay - first.ElectricityDay) * rate[3] : 0;
        eeNight = ElectricityNight != 0 ? (ElectricityNight - first.ElectricityNight) * rate[4] : 0;
        ee = Electricity != 0 ? (Electricity - first.Electricity) * rate[2] : double.Parse(Living) * standard[2] * rate[2];
        sum = (Math.Round(xvs + gvsCoolant + gvsThermalEnergy + gvs + eeDay + eeNight + ee,3)).ToString();
        first = db.Testimonys.OrderBy(e => e.Id).LastOrDefault();
    });

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
