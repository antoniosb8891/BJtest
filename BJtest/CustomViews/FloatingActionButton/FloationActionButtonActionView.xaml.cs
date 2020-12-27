using Xamarin.Forms;

namespace BJtest.CustomViews.FloatingActionButton {
    public partial class FloationActionButtonActionView : ContentView {

        public FloatingActionButtonView FloatingButton { private get; set; }

        private FloatingActionButtonAction _floatingAction;
        public FloatingActionButtonAction FloatingAction {
            set {
                _floatingAction = value;
                SetAction(value);
            }
        }

        public FloationActionButtonActionView() {
            InitializeComponent();
            ActionButton.Clicked += (sender, e) => {
                Click();
            };
        }

        private void SetAction(FloatingActionButtonAction action) {
            if (action != null) {
                ActionLabel.Text = action.Title;
                ActionButton.Source = ImageSource.FromFile(action.Icon);

                if (!action.BackGroundColorActionButton.Equals(Color.Default)) {
                    ActionButtonFrame.BackgroundColor = action.BackGroundColorActionButton;
                    ActionButtonFrame.BorderColor = action.BackGroundColorActionButton;
                }
            }
        }

        void Label_Tapped(object sender, System.EventArgs e) {
            Click();
        }

        private void Click() {
            _floatingAction?.ButtonAction?.Invoke();
            FloatingButton?.ShowHideButtons();
        }
    }
}
