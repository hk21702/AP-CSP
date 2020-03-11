using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Drawing;
//using System.Windows.Forms;
using Windows.Graphics.Display;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
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
        double angle = Math.PI/4,
            angleAccel,
            angleVelocity = 0,
            dt = 0.1;

        int anchorX,
            anchorY,
            ballX,
            ballY;

        int length = 300;
        int mass = 50;
        double gravity = -9.81;

        Windows.Foundation.Size window;

        Line line = new Line();
        Ellipse ball = new Ellipse();
        public MainPage()
        {
            this.InitializeComponent();

            var autoEvent = new AutoResetEvent(false);

            var visibleBounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            window = new Windows.Foundation.Size(visibleBounds.Width * scaleFactor, visibleBounds.Height * scaleFactor);

            updatePositions();

            line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line.X1 = anchorX;
            line.Y1 = anchorY;
            line.X2 = ballX;
            line.Y2 = ballY;

            ball.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            ball.Width = mass;
            ball.Height = mass;
            Canvas.SetLeft(ball, ballX-(ball.Width/2));
            Canvas.SetTop(ball, ballY-(ball.Height/2));

            canvas.Children.Add(line);
            canvas.Children.Add(ball);

            CompositionTarget.Rendering += Render;
           // System.Threading.Timer timing = new System.Threading.Timer(timerTick, null, 0, 10);           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
           
          

        }

        private async void timerTick(object stateInfo)
        {

          await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, ()=>{
               
                    

                    Canvas.SetLeft(ball, ballX - (ball.Width / 2));
                    Canvas.SetTop(ball, ballY - (ball.Height / 2));

                    line.X2 = ballX;
                    line.Y2 = ballY;

                
            });
            updatePositions();
  
        }

        private void Render(object sender, object e)
        {
            Canvas.SetLeft(ball, ballX - (ball.Width / 2));
            Canvas.SetTop(ball, ballY - (ball.Height / 2));

            line.X2 = ballX;
            line.Y2 = ballY;

            updatePositions();
        }

        public void updatePositions()
        {
            anchorX = (int)(window.Width / 2) - 12;
            anchorY = (int)(window.Height / 4);
            ballX = anchorX + (int)(Math.Sin(angle) * length);
            ballY = anchorY + (int)(Math.Cos(angle) * length);

            angleAccel = (gravity / length * Math.Sin(angle));
            angleVelocity += angleAccel * dt;
            angle += angleVelocity * dt;
        }
    }
}
