using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Swing
{
public partial class Pendulum : Window
{
private double PendulumMass = 1;
private double PendulumLength = 1;
private double DampingCoefficient = 0.5;
private double Theta0 = 45;
private double Alpha0 = 0;
double[] xx = new double[2];
double time = 0;
double dt = 0.03;
Polyline pl = new Polyline();
double xMin = 0;
Double yMin = -100;
double xMax = 50;
double yMax = 100;
public Pendulum()
{
InitializeComponent();
}
private void btnStart_Click(
object sender, RoutedEventArgs e)
{
PendulumMass = Double.Parse(tbMass.Text);
PendulumLength = Double.Parse(tbLength.Text);
DampingCoefficient = Double.Parse(tbDamping.Text);
Theta0 = Double.Parse(tbTheta0.Text);
Theta0 = Math.PI * Theta0 / 180;
Alpha0 = Double.Parse(tbAlpha0.Text);
Alpha0 = Math.PI * Alpha0 / 180;
tbDisplay.Text = "Starting...";
if (canvasRight.Children.Count > 4)
canvasRight.Children.Remove(pl);

pl = new Polyline();
pl.Stroke = Brushes.Red;
canvasRight.Children.Add(pl);
time = 0;
xx = new double[2] { Theta0, Alpha0 };
CompositionTarget.Rendering += StartAnimation;
}
private void StartAnimation(
object sender, EventArgs e)
{
// Invoke ODE solver:
ODESolver.Function[] f =
new ODESolver.Function[2] { f1, f2 };
double[] result = ODESolver.RungeKutta4(
f, xx, time, dt);
// Display moving pendulum on screen:
Point pt = new Point(
140 + 130 * Math.Sin(result[0]),
20 + 130 * Math.Cos(result[0]));
ball.Center = pt;
line1.X2 = pt.X;
line1.Y2 = pt.Y;
// Display theta - time curve on canvasRight:
if (time < xMax)
pl.Points.Add(new Point(XNormalize(time) + 10,
YNormalize(180 * result[0] / Math.PI)));
// Reset the initial values for next calculation:
xx = result;
time += dt;
if (time > 0 && Math.Abs(result[0]) < 0.01 &&
Math.Abs(result[1]) < 0.001)
{
tbDisplay.Text = "Stopped";
CompositionTarget.Rendering -= StartAnimation;
}
}
private void btnReset_Click(
object sender, RoutedEventArgs e)
{
PendulumInitialize();
tbDisplay.Text = "Stopped";
if (canvasRight.Children.Count > 4)
canvasRight.Children.Remove(pl);
CompositionTarget.Rendering -= StartAnimation;
}

private void PendulumInitialize()
{
tbMass.Text = "1";
tbLength.Text = "1";
tbDamping.Text = "0.1";
tbTheta0.Text = "45";
tbAlpha0.Text = "0";
line1.X2 = 140;
line1.Y2 = 150;
ball.Center = new Point(140, 150);
}
private void btnStop_Click(
object sender, RoutedEventArgs e)
{
line1.X2 = 140;
line1.Y2 = 150;
ball.Center = new Point(140, 150);
tbDisplay.Text = "Stopped";
CompositionTarget.Rendering -= StartAnimation;
}
private double f1(double[]xx, double t)
{
return xx[1];
}
private double f2(double[] xx, double t)
{
double m = PendulumMass;
double L = PendulumLength;
double g = 9.81;
double b = DampingCoefficient;
return -g * Math.Sin(xx[0]) / L - b * xx[1] / m;
}
private double XNormalize(double x)
{
double result = (x - xMin) *
canvasRight.Width / (xMax - xMin);
return result;
}
private double YNormalize(double y)
{
double result = canvasRight.Height - (y - yMin) *
canvasRight.Height / (yMax - yMin);
return result;
}
}
}