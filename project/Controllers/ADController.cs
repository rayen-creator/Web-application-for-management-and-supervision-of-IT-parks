using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Models;

namespace project.Controllers
{
    [Authorize]
    public class ADController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Find(FindViewModel model)
        //{
        //    var entry = new DirectoryEntry("LDAP://server.local,Administrator,1920rayenCa");

        //    var searcher = new DirectorySearcher(entry)
        //    {
        //        Filter = $"(&(objectClass=user)(objectCategory=person)(sAMAccountName=username))",
        //        PropertiesToLoad = { "displayName" }
        //    };

        //    var result = searcher.FindOne();

        //    if (result != null)
        //    {
        //        string displayName = (string)result.Properties["displayname"][0];
        //        return View(ViewBag.displayName);
        //    }

        //    return View();
        //}

        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> ADUsers = GetallAdUsers();
                return View(ADUsers);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            
        }

        public IActionResult GetAllGroups()
        {
            try
            {
                List<Group> ADGroups = GetallGroups();
                return View(ADGroups);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }

        public IActionResult ChangePasswd()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }

        public IActionResult Workgroup()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }
        public IActionResult Groupuser()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }

        #region User
        public static List<User> GetallAdUsers()
        {
            List<User> AdUsers = new List<User>();
            try
            {
                var ctx = new PrincipalContext(ContextType.Domain, null);
                UserPrincipal userPrin = new UserPrincipal(ctx);
                userPrin.Name = "*";
                var searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher();
                searcher.QueryFilter = userPrin;
                var results = searcher.FindAll();
                foreach (Principal p in results)
                {
                    AdUsers.Add(new User
                    {
                        DisplayName = p.DisplayName,
                        Samaccountname = p.SamAccountName,
                        Description = p.Description
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return AdUsers;
        }

        public IActionResult ResetPasswordWithfield(string Samaccountname, string newPassword)
        {
            try
            {
                //i get the user by its SamaccountName to change his password
                PrincipalContext context = new PrincipalContext(ContextType.Domain, null);
                UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Samaccountname);
                //Enable Account if it is disabled
                user.Enabled = true;
                //Reset User Password
                user.SetPassword(newPassword);
                //Force user to change password at next logon dh optional
                user.ExpirePasswordNow();
                user.Save();
                TempData["msg"] = "<script>alert('Password Changed Successfully');</script>";
            }
            catch (Exception e)
            {
                return View("Error");
            }

            return RedirectToAction("GetAllUsers");

        }

        public IActionResult ResetPassword(string Samaccountname)
        {
            //i get the user by its SamaccountName to change his password
            PrincipalContext context = new PrincipalContext(ContextType.Domain, null);
            UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Samaccountname);

            ////Enable Account if it is disabled
            user.Enabled = true;
            //Reset User Password
            string newPassword = "P@ssw0rd";
            user.SetPassword(newPassword);
            //Force user to change password at next logon dh optional
            user.ExpirePasswordNow();
            user.Save();
            
            TempData["msg"] = "<script>alert('Password Changed Successfully');</script>";
            return RedirectToAction("GetAllUsers");
        }

        public IActionResult DeleteUser(string Samaccountname)
        {
            //i get the user by its SamaccountName to change his password
            PrincipalContext context = new PrincipalContext(ContextType.Domain, null);
            UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Samaccountname);

            if ((user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Admins du domaine"))) || (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Administrateurs"))))
            {
                TempData["msg"] = "<script>alert('This user has high privilege ; Log out or try contact the server manager');</script>";
            }
            else
            {
                user.Delete();
                TempData["msg"] = "<script>alert('Delete confirmed');</script>";

            }

            return RedirectToAction("GetAllUsers");
        }
        #endregion

        #region Group
        public IActionResult DeleteGroup(string groupName)
        {
           
            try
            {
                var SecurityContext = new PrincipalContext(ContextType.Domain, null);
                var TheGroup = GroupPrincipal.FindByIdentity(SecurityContext, groupName);
                if ((TheGroup.DisplayName== "Administrateurs") || (TheGroup.DisplayName == "Admins du domaine") || (TheGroup.DisplayName == "Utilisateurs du domaine") || (TheGroup.DisplayName == "TSI"))
  
                {
                    TempData["msg"] = "<script>alert('Group can't be deleted');</script>";

                }
                else
                {
                    TheGroup.Delete();
                    TempData["msg"] = "<script>alert('Delete Confirmed');</script>";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            return RedirectToAction("GetAllGroups");

        }

        public IActionResult AddUserToGroup(string Samaccountname, string groupName)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, null);
                
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                    group.Members.Add(context, IdentityType.SamAccountName, Samaccountname);
                    group.Save();
                    TempData["msg"] = "<script>alert('User Added to group Successfully');</script>";

                
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("GetAllUsers");

        }

        public IActionResult RemoveUserFromGroup(string Samaccountname, string groupName)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, null);
               
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                    group.Members.Remove(context, IdentityType.SamAccountName, Samaccountname);
                    group.Save();
                    TempData["msg"] = "<script>alert('User Deleted from group Successfully');</script>";

                
            }
            catch (Exception e)
            {
                return View("Error");
            }

            return RedirectToAction("GetAllUsers");

        }

        public static List<Group> GetallGroups()
        {
            List<Group> AdGroups = new List<Group>();
            try
            {
                var ctx = new PrincipalContext(ContextType.Domain,null);
                GroupPrincipal _groupPrincipal = new GroupPrincipal(ctx);

                PrincipalSearcher srch = new PrincipalSearcher(_groupPrincipal);

                foreach (var found in srch.FindAll())
                {
                    AdGroups.Add(new Group {
                        GroupName = found.ToString(),
                        Description = found.Description
                    });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);

            }
            return AdGroups;
        }
        #endregion
    }
}