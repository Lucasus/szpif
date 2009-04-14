using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SzpifbaseBuilder
{
    public class ViewPermissions
    {
        List<string> selectPermissionsByRole;
        List<string> updatePermissionsByRole;
        List<string> insertPermissionsByRole;
        List<string> deletePermissionsByRole;

        public List<string> SelectPermissionsByRole
        {
            get { return selectPermissionsByRole; }
        }

        public List<string> UpdatePermissionsByRole
        {
            get { return updatePermissionsByRole; }
        }

        public List<string> InsertPermissionsByRole
        {
            get { return insertPermissionsByRole; }
        }

        public List<string> DeletePermissionsByRole
        {
            get { return deletePermissionsByRole; }
        }

        public ViewPermissions()
        {
            selectPermissionsByRole = new List<string>();
            insertPermissionsByRole = new List<string>();
            deletePermissionsByRole = new List<string>();
            updatePermissionsByRole = new List<string>();
        }

        public void Add(string permissionName, string roleName)
        {
            switch (permissionName)
            {
                case "select": SelectPermissionsByRole.Add(roleName);
                    break;
                case "insert": SelectPermissionsByRole.Add(roleName);
                    break;
                case "update": SelectPermissionsByRole.Add(roleName);
                    break;
                case "delete": SelectPermissionsByRole.Add(roleName);
                    break;

            };
        }

        public void Delete(string permissionName, string roleName)
        {
            switch (permissionName)
            {
                case "select": SelectPermissionsByRole.Remove(roleName);
                    break;
                case "insert": SelectPermissionsByRole.Remove(roleName);
                    break;
                case "update": SelectPermissionsByRole.Remove(roleName);
                    break;
                case "delete": SelectPermissionsByRole.Remove(roleName);
                    break;

            };
        }
    }
}
