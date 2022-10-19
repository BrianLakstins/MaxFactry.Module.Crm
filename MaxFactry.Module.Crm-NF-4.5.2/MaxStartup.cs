﻿// <copyright file="MaxModule.cs" company="Lakstins Family, LLC">
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
// <change date="7/8/2015" author="Brian A. Lakstins" description="Initial creation">
// <change date="6/5/2020" author="Brian A. Lakstins" description="Remove unneeded config">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm
{
    using System;

    public class MaxStartup : MaxFactry.Base.MaxStartup
    {
        /// <summary>
        /// Internal storage of single object
        /// </summary>
        private static object _oInstance = null;

        /// <summary>
        /// Gets the single instance of this class.
        /// </summary>
        public new static MaxStartup Instance
        {
            get
            {
                _oInstance = CreateInstance(typeof(MaxStartup), _oInstance);
                return _oInstance as MaxStartup;
            }
        }

        public override void RegisterProviders()
        {
            MaxFactry.Module.Crm.BusinessLayer.MaxCrmLibrary.Instance.ProviderAdd(typeof(MaxFactry.Module.Crm.BusinessLayer.Provider.MaxCrmLibraryDefaultProvider));
        }

        public override void SetProviderConfiguration(MaxFactry.Core.MaxIndex loConfig)
        {
        }

        public override void ApplicationStartup()
        {
        }
    }
}