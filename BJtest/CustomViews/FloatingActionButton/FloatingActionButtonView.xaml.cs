using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BJtest.CustomViews.FloatingActionButton {
    public partial class FloatingActionButtonView : ContentView {

        private const int MAX_WIDTH = 300;

        bool containerButtonsExpanded = false;
        bool disableRotation = false;

        private List<FloatingActionButtonAction> Actions = new List<FloatingActionButtonAction>();

        public event EventHandler MainButtonClicked;

        public FloatingActionButtonView() {
            InitializeComponent();
            MainButton.Clicked += (sender, e) => {
                if (Actions != null && Actions.Count > 0) {
                    ShowHideButtons();
                } else {
                    MainButtonClicked?.Invoke(this, new EventArgs());
                }
            };
        }

        public void SetActions(List<FloatingActionButtonAction> actions) {
            if (actions != null) {
                Actions = actions;
                Container.Children.Clear();
                foreach (var a in Actions) {
                    var actionView = new FloationActionButtonActionView() {
                        FloatingAction = a,
                        FloatingButton = this,
                        HorizontalOptions = LayoutOptions.End
                    };
                    Container.Children.Add(actionView);
                }
            }
        }

        public void SetIcon(string fileName) {
            MainButton.Source = fileName;
            disableRotation = true;
        }

        public async Task ScaleButton() {
            await MainButton.ScaleTo(1.1, 500, Easing.CubicIn);
            await MainButton.ScaleTo(1, 500, Easing.CubicOut);
        }

        public async void ShowHideButtons() {
            containerButtonsExpanded = !containerButtonsExpanded;
            var targetOpacity = 0;
            var targetHeight = 0;
            var targetWidth = 0;
            if (containerButtonsExpanded) {
                targetOpacity = 1;
                targetHeight = Actions.Count * 44 + (Actions.Count * 10);
                targetWidth = MAX_WIDTH;
            }

            if (!disableRotation) {
                if (containerButtonsExpanded) {
                    await MainButton.RotateTo(45, 100, Easing.Linear);
                } else {
                    await MainButton.RotateTo(0, 100, Easing.Linear);
                }
            }

            if (containerButtonsExpanded) {
                Container.HeightRequest = targetHeight;
                Container.WidthRequest = targetWidth;
            }
            var taskList = new List<Task> {
                Container.FadeTo(targetOpacity, 150)
            };
            await Task.WhenAny(taskList);
            if (!containerButtonsExpanded) {
                Container.HeightRequest = targetHeight;
                Container.WidthRequest = targetWidth;
            }
        }
    }
}