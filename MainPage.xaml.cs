using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Drawing;
//using System.Windows.Forms;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AP_CSP
{
    public sealed partial class MainPage : Page
    {
        Timer timer = new Timer {Interval = 30};
        double angle = Math.PI / 2,
            angleAccel,
            angleVelocity = 0,
            dt = 0.1;

        int length = 50;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {

            timer.Enabled = true;

            
            
            timer.Tick += delegate(object sender, EventArgs e)
            {
                int anchorX = (Canvas.WidthProperty / 2) - 12,
                    anchorY = (Canvas.HeightProperty / 4),
                    ballX = anchorX + (int)(Math.Sin(angle) * length),
                    ballY = anchorY + (int)(Math.Cos(angle) * length);


            };

            timer.Start();

        }
    }
}
