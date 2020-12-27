using System;
using Xamarin.Forms;

namespace BJtest.CustomViews.FloatingActionButton {
    public class FloatingActionButtonAction {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Action ButtonAction { get; set; }
        public Color BackGroundColorActionButton { get; set; }
    }
}
