using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TheXamlGuy.UI.WPF.Controls;

public class Arc : Shape
{
    public static readonly DependencyProperty DirectionProperty =
        DependencyProperty.Register(nameof(Direction),
            typeof(SweepDirection), typeof(Arc), new PropertyMetadata(SweepDirection.Clockwise));

    public static readonly DependencyProperty EndAngleProperty =
        DependencyProperty.Register(nameof(EndAngle),
            typeof(double), typeof(Arc), new PropertyMetadata(0.0d, OnEndAnglePropertyChanged));

    public static readonly DependencyProperty OriginRotationDegreesProperty =
        DependencyProperty.Register(nameof(OriginRotationDegrees),
            typeof(double), typeof(Arc), new PropertyMetadata(270.0, new PropertyChangedCallback(OnOriginRotationDegreesPropertyChanged)));

    public static readonly DependencyProperty StartAngleProperty =
        DependencyProperty.Register(nameof(StartAngle),
            typeof(double), typeof(Arc), new PropertyMetadata(0.0d, OnStartAnglePropertyChanged));

    public SweepDirection Direction
    {
        get => (SweepDirection)GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    public double OriginRotationDegrees
    {
        get => (double)GetValue(OriginRotationDegreesProperty);
        set => SetValue(OriginRotationDegreesProperty, value);
    }

    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    protected override Geometry DefiningGeometry => GetDefiningGeometry();

    protected override Size MeasureOverride(Size constraint)
    {
        return constraint;
    }

    private static void OnEndAnglePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as Arc)?.OnEndAnglePropertyChanged();
    }

    private static void OnOriginRotationDegreesPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as Arc)?.OnOriginRotationDegreesPropertyChanged();
    }

    private static void OnStartAnglePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as Arc)?.OnStartAnglePropertyChanged();
    }

    private Geometry GetDefiningGeometry()
    {
        Point startPoint = PointAtAngle(Math.Min(StartAngle, EndAngle), Direction);
        Point endPoint = PointAtAngle(Math.Max(StartAngle, EndAngle), Direction);

        Size arcSize = new(Math.Max(0, (ActualWidth - StrokeThickness) / 2), Math.Max(0, (ActualHeight - StrokeThickness) / 2));
        bool isLargeArc = Math.Abs(EndAngle - StartAngle) > 180;

        StreamGeometry streamGeometry = new();
        using (StreamGeometryContext context = streamGeometry.Open())
        {
            context.BeginFigure(startPoint, false, false);
            context.ArcTo(endPoint, arcSize, 0, isLargeArc, Direction, true, false);
        }

        streamGeometry.Transform = new TranslateTransform(StrokeThickness / 2, StrokeThickness / 2);
        return streamGeometry;
    }

    private void OnEndAnglePropertyChanged()
    {
        InvalidateVisual();
    }

    private void OnOriginRotationDegreesPropertyChanged()
    {
        InvalidateVisual();
    }

    private void OnStartAnglePropertyChanged()
    {
        InvalidateVisual();
    }


    private Point PointAtAngle(double angle, SweepDirection sweep)
    {
        double translatedAngle = angle + OriginRotationDegrees;
        double radAngle = translatedAngle * (Math.PI / 180);
        double xr = (RenderSize.Width - StrokeThickness) / 2;
        double yr = (RenderSize.Height - StrokeThickness) / 2;

        double x = xr + xr * Math.Cos(radAngle);
        double y = yr * Math.Sin(radAngle);

        y = sweep == SweepDirection.Counterclockwise ? yr - y : yr + y;
        return new Point(x, y);
    }
}