using System;
using System.Collections.Generic;
using SOh_ParkInspect.Enum;

namespace SOh_ParkInspect.Helper
{
    public class PermissionsMap
    {
        public static readonly List<PermissionsMap> Controls = new List<PermissionsMap>
        {
            new PermissionsMap(ControlType.ClientInfoControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.CreateTaskControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.EditTemplateControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.EmployeeInfoControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.TaskOverviewControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.TaskDetailsControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.ManagementReportControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.PerformInspectionControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.TemplateOverviewControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.RegisterInspectorControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.UserManagementControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.LocationInfoControl, new List<RoleType> { RoleType.Admin, RoleType.Manager }),
            new PermissionsMap(ControlType.ManagementControl, new List<RoleType> { RoleType.Admin, RoleType.Manager })
        };

        public ControlType Control { get; private set; }
        public List<RoleType> Roles { get; private set; }

        private PermissionsMap(ControlType control, List<RoleType> roles)
        {
            Control = control;
            Roles = roles;
        }
    }
}