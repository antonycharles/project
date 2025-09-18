using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Management.Web.Helpers
{
    public class RoleConstants
    {
        protected const string code = "4";
        protected const string list = "list";
        protected const string create = "create";
        protected const string update = "update";
        protected const string delete = "delete";


        public class AppRole{
            private const string prefix = "app";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }

        public class ClientRole{
            private const string prefix = "client";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }

        public class ClientProfileRole{
            private const string prefix = "client-profile";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }

        public class PermissionRole{
            private const string prefix = "permission";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }

        public class ProfileRole{
            private const string prefix = "profile";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }

        public class UserRole{
            private const string prefix = "user";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }
        
        public class UserProfileRole{
            private const string prefix = "user-profile";
            public const string List = $"{code}-{prefix}-{list}";
            public const string Create = $"{code}-{prefix}-{create}";
            public const string Update = $"{code}-{prefix}-{update}";
            public const string Delete = $"{code}-{prefix}-{delete}";
        }
    }
}