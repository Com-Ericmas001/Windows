using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Com.Ericmas001.Windows.Xaml.Effects
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AngularGradientEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(
            "Input",
            typeof(AngularGradientEffect),
            0);

        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register(
            "CenterPoint",
            typeof(Point),
            typeof(AngularGradientEffect),
            new UIPropertyMetadata(new Point(0.2D, 0.5D), PixelShaderConstantCallback(0)));

        public static readonly DependencyProperty PrimaryColorProperty = DependencyProperty.Register(
            "PrimaryColor",
            typeof(Color),
            typeof(AngularGradientEffect),
            new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 255), PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty SecondaryColorProperty = DependencyProperty.Register(
            "SecondaryColor",
            typeof(Color),
            typeof(AngularGradientEffect),
            new UIPropertyMetadata(Color.FromArgb(255, 255, 0, 0), PixelShaderConstantCallback(2)));

        public static readonly DependencyProperty ThirdColorProperty = DependencyProperty.Register(
            "ThirdColor",
            typeof(Color),
            typeof(AngularGradientEffect),
            new UIPropertyMetadata(Color.FromArgb(255, 0, 255, 0), PixelShaderConstantCallback(3)));

        public AngularGradientEffect()
        {
            PixelShader pixelShader = new PixelShader
            {
                UriSource = new Uri("/Com.Ericmas001.Windows.Xaml;component/Resources/AngularGradientEffect.ps", UriKind.Relative)
            };
            PixelShader = pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CenterPointProperty);
            UpdateShaderValue(PrimaryColorProperty);
            UpdateShaderValue(SecondaryColorProperty);
            UpdateShaderValue(ThirdColorProperty);
        }

        public Brush Input
        {
            get { return ((Brush) (GetValue(InputProperty))); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>The center of the gradient. </summary>
        public Point CenterPoint
        {
            get { return ((Point) (GetValue(CenterPointProperty))); }
            set { SetValue(CenterPointProperty, value); }
        }

        /// <summary>The primary color of the gradient. </summary>
        public Color PrimaryColor
        {
            get { return ((Color) (GetValue(PrimaryColorProperty))); }
            set { SetValue(PrimaryColorProperty, value); }
        }

        /// <summary>The secondary color of the gradient. </summary>
        public Color SecondaryColor
        {
            get { return ((Color) (GetValue(SecondaryColorProperty))); }
            set { SetValue(SecondaryColorProperty, value); }
        }

        /// <summary>The third color of the gradient. </summary>
        public Color ThirdColor
        {
            get { return ((Color) (GetValue(ThirdColorProperty))); }
            set { SetValue(ThirdColorProperty, value); }
        }
    }
}
