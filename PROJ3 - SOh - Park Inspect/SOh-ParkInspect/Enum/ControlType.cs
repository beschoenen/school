using System;
using SOh_ParkInspect.Control;

namespace SOh_ParkInspect.Enum
{
    public class ControlType
    {
        ////////
        // Types

        public static ControlType AccountControl => new ControlType(typeof(AccountControl));
        public static ControlType ChangePasswordControl => new ControlType(typeof(ChangePasswordControl));
        public static ControlType ClientInfoControl => new ControlType(typeof(ClientInfoControl));
        public static ControlType CreateTaskControl => new ControlType(typeof(CreateTaskControl));
        public static ControlType EditTemplateControl => new ControlType(typeof(EditTemplateControl));
        public static ControlType EmployeeInfoControl => new ControlType(typeof(EmployeeInfoControl));
        public static ControlType TaskDetailsControl => new ControlType(typeof(TaskDetailsControl));
        public static ControlType TaskOverviewControl => new ControlType(typeof(TaskOverviewControl));
        public static ControlType ManagementReportControl => new ControlType(typeof(ManagementReportControl));
        public static ControlType PerformInspectionControl => new ControlType(typeof(PerformInspectionControl));
        public static ControlType RegisterInspectorControl => new ControlType(typeof(RegisterInspectorControl));
        public static ControlType TemplateOverviewControl => new ControlType(typeof(TemplateOverviewControl));
        public static ControlType UserManagementControl => new ControlType(typeof(UserManagementControl));
        public static ControlType LocationInfoControl => new ControlType(typeof(LocationInfoControl));
        public static ControlType ManagementControl => new ControlType(typeof(ManagementControl));
        public static ControlType ExecuteInspectionControl => new ControlType(typeof(ExecuteInspectionControl));
        public static ControlType HeatMapControl => new ControlType(typeof(HeatMapControl));
        public static ControlType ClientOverviewControl => new ControlType(typeof(ClientOverviewControl));
        public static ControlType LinkInspectorControl => new ControlType(typeof(LinkInspectorControl));

        public static ControlType InspectionOverview => new ControlType(typeof(InspectionOverviewControl));
        public static ControlType EmployeeAvailabilityControl => new ControlType(typeof(EmployeeAvailabilityControl));

        //////////////
        // Constructor

        public Type Control { get; private set; }

        private ControlType(Type control)
        {
            Control = control;
        }
    }
}
