using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Api.Helpers
{
    public class RoleConstants
    {
        protected const string list = "list";
        protected const string create = "create";
        protected const string update = "update";
        protected const string delete = "delete";


        public class AppRole{
            private const string prefix = "app";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }

        public class ClientRole{
            private const string prefix = "client";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }

        public class ClientProfileRole{
            private const string prefix = "client-profile";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }

        public class PermissionRole{
            private const string prefix = "permission";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }

        public class ProfileRole{
            private const string prefix = "profile";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }

        public class UserAuthorizationRole{
            private const string prefix = "user-authorization";
            public const string Authorization = $"{prefix}";
        }

        public class UserRole{
            private const string prefix = "user";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }
        
        public class UserProfileRole{
            private const string prefix = "user-profile";
            public const string List = $"{prefix}-{list}";
            public const string Create = $"{prefix}-{create}";
            public const string Update = $"{prefix}-{update}";
            public const string Delete = $"{prefix}-{delete}";
        }
    }
}