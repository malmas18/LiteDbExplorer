﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDbExplorer
{
    public class Settings : INotifyPropertyChanged
    {
        public class WindowPosition
        {
            public class Point
            {
                public double X
                {
                    get; set;
                }

                public double Y
                {
                    get; set;
                }
            }

            public Point Position
            {
                get; set;
            }

            public Point Size
            {
                get; set;
            }
        }

        private Dictionary<string, WindowPosition> windowPositions = new Dictionary<string, WindowPosition>();
        public Dictionary<string, WindowPosition> WindowPositions
        {
            get
            {
                return windowPositions;
            }

            set
            {
                windowPositions = value;
                OnPropertyChanged("WindowPositions");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static Settings LoadSettings()
        {
            if (File.Exists(Paths.SettingsFilePath))
            {
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Paths.SettingsFilePath));
            }
            else
            {
                return new Settings();
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(Paths.SettingsFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
