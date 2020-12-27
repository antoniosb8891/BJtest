using System;
using System.ComponentModel;
using BJtest.CustomViews.ShadowFrame;
using BJtest.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))]
namespace BJtest.iOS.Renderers {
    public class ShadowFrameRenderer : FrameRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e) {
            base.OnElementChanged(e);
            if (e.OldElement == null) {
            }

            
        }

        private void SetupLayer() {
            Layer.ShadowRadius = 4.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(1, 4);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRoundedRect(Layer.Bounds, Element.CornerRadius).CGPath;
            Layer.MasksToBounds = false;
        }

        public override void LayoutSubviews() {
            base.LayoutSubviews();
            SetupLayer();
        }
    }
}
