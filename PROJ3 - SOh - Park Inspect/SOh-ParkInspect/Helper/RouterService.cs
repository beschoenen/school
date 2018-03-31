using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using SOh_ParkInspect.Converter;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Model;

namespace SOh_ParkInspect.Helper
{
    public class RouterService : ObservableObject
    {
        private readonly Stack<ParkInspectControl> _routingStack;

        public ParkInspectControl CurrentView { get; private set; }

        public RouterService()
        {
            _routingStack = new Stack<ParkInspectControl>();
        }

        /// <summary>
        ///     Check if you are at the root.
        /// </summary>
        /// <returns>If you are at the root</returns>
        public bool IsAtRoot()
        {
            return _routingStack.Count == 0;
        }

        /// <summary>
        ///     Go to the previous control.
        /// </summary>
        public void GoBack()
        {
            if (IsAtRoot()) return;

            GoTo(_routingStack.Pop(), true);
        }

        /// <summary>
        ///     Navigate to a specific view.
        /// </summary>
        /// <param name="control">The name of the control without "Control"</param>
        /// <param name="viewBag">Object to pass to the control</param>
        /// <param name="clearHistory">Default false; If true the history will be cleared before navigating to the new control</param>
        public void GoTo(ControlType control, ViewBag viewBag = null, bool clearHistory = false)
        {
            if (!CanAccess(control))
            {
                MessageBox.Show("Je hebt geen rechten tot dit deel van de applicatie.", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var userControl = (ParkInspectControl) Activator.CreateInstance(control.Control);
            userControl.ViewBag = viewBag;

            GoTo(userControl, false, clearHistory);
        }

        private bool CanAccess(ControlType control)
        {
            var permission = PermissionsMap.Controls.FirstOrDefault(c => c.Control.Control == control.Control);

            if (permission == null || !permission.Roles.Any()) return true;

            var role = RoleEnumConverter.Convert(Settings.CurrentUser.JobTitle.Name);

            return permission.Roles.Contains(role);
        }

        private void GoTo(ParkInspectControl control, bool goingBack = false, bool clearHistory = false)
        {
            // Stop if trying to navigate to the same view
            if (CurrentView?.GetType() == control.GetType()) return;

            // Push the current view to the stack (only if it's set)
            if (CurrentView != null && !goingBack)
            {
                _routingStack.Push(CurrentView);
            }

            if (clearHistory) _routingStack.Clear();

            ((ParkInspectViewModel) CurrentView?.DataContext)?.OnLeave();

            CurrentView = control;

            ((ParkInspectViewModel) CurrentView.DataContext).OnEnter();

            RaisePropertyChanged(nameof(CurrentView));
        }
    }
}