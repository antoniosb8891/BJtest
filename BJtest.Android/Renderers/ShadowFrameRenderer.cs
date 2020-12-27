using Android.Content;
using Xamarin.Forms;
using BJtest.CustomViews.ShadowFrame;
using BJtest.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;
using Android.Util;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))]
namespace BJtest.Droid.Renderers {
    public class ShadowFrameRenderer : FrameRenderer {

        public ShadowFrameRenderer(Context context) : base(context) {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e) {
            base.OnElementChanged(e);
            if(e.OldElement == null && Control != null) {
                Control.Elevation = TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Context.Resources.DisplayMetrics);
            }
        }
    }
}
