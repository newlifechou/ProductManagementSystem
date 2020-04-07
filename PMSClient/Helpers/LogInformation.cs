﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserSimpleService;

namespace PMSClient.Helper
{
    /// <summary>
    /// 存储和处理客户端所需要的用户信息
    /// </summary>
    public class LogInformation
    {
        public DcUser CurrentUser { get; set; }
        public DcUserRole CurrentUserRole { get; set; }
        public List<DcUserAccess> CurrentAccesses { get; set; }
        public LogInformation()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses = new List<DcUserAccess>();
        }
        /// <summary>
        /// 注销
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses.Clear();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogIn(string username, string password)
        {
            try
            {
                using (var service = new UserSimpleServiceClient())
                {
                    CurrentUser = service.GetUser(username, password);
                    if (CurrentUser != null)
                    {
                        CurrentUserRole = service.GetRole(CurrentUser.RoleID);

                        var accesses = service.GetAccesses(CurrentUser.RoleID);
                        CurrentAccesses.Clear();
                        accesses.ToList().ForEach(i => CurrentAccesses.Add(i));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsAuthorized(string accessCode)
        {
            if (CurrentUser == null)
            {
                return false;
            }
            return CurrentAccesses.Where(i => i.AccessCode == accessCode).Count() > 0;
        }

        public bool IsInGroup(string[] groups)
        {
            if (groups == null) return false;
            if (groups.Length == 0) return false;
            if (CurrentUserRole != null)
            {
                foreach (var item in groups)
                {
                    if (CurrentUserRole.GroupName.Contains(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 从数据库里根据控制名来查询组字符串
        /// </summary>
        /// <param name="controlname"></param>
        /// <returns></returns>
        public string[] GetRoleGroupByControlName(string controlname)
        {
            try
            {
                using (var userRole = new UserSimpleServiceClient())
                {
                    string roleStr = userRole.GetAccessGrantByControl(controlname);
                    return GetRoles(roleStr);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// 将数据库里+连接的用户组名转换为数组
        /// </summary>
        /// <param name="roleStr"></param>
        /// <returns></returns>
        private string[] GetRoles(string roleStr)
        {
            if (string.IsNullOrEmpty(roleStr))
            {
                return null;
            }
            string[] roles = roleStr.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }

        public bool IsLogIn()
        {
            return CurrentUser != null;
        }
    }
}
