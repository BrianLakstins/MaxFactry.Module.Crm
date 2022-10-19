// <copyright file="MaxCmsController.cs" company="Lakstins Family, LLC">
// Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// </copyright>

#region License
// <license>
// This software is provided 'as-is', without any express or implied warranty. In no 
// event will the author be held liable for any damages arising from the use of this 
// software.
//  
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to the 
// following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim that 
// you wrote the original software. If you use this software in a product, an 
// acknowledgment (see the following) in the product documentation is required.
// 
// Portions Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// 
// 2. Altered source versions must be plainly marked as such, and must not be 
// misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.
// </license>
#endregion

#region Change Log
// <changelog>
// <change date="6/3/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/19/2014" author="Brian A. Lakstins" description="Making controller thinner.">
// <change date="8/13/2014" author="Brian A. Lakstins" description="Added logging.">
// <change date="8/21/2014" author="Brian A. Lakstins" description="Update CMS editing and storage.">
// <change date="9/30/2014" author="Brian A. Lakstins" description="Update based on OutputCache testing.">
// <change date="10/6/2014" author="Brian A. Lakstins" description="Update index to handle any request method (GET, POST, HEAD)">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.Mvc4.PresentationLayer
{

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MaxFactry.Core;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;

    [MaxAuthorize(Order = 2)]
    public class MaxCrmController : MaxBaseController
    {
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            ViewBag.Message = "Hello World Template MVC";
            return View();
        }

        [HttpGet]
        public virtual ActionResult Interaction(string id)
        {
            MaxCrmInteractionViewModel loModel = new MaxCrmInteractionViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Interaction(MaxCrmInteractionViewModel loModel, string uoProcess)
        {
            if (uoProcess.Equals(MaxManageController.ProcessCreate, StringComparison.InvariantCultureIgnoreCase))
            {
                Guid loId = loModel.SaveNew();
                return RedirectToAction("Interaction", routeValues: new { id = loId });
            }
            else if (uoProcess.Equals(MaxManageController.ProcessSave, StringComparison.InvariantCultureIgnoreCase))
            {
                loModel.Save();
            }
            else if (uoProcess.Equals(MaxManageController.ProcessCancel, StringComparison.InvariantCultureIgnoreCase))
            {
                return RedirectToAction("Interaction");
            }

            return View(loModel);
        }
    }
}
