// <copyright file="MaxCrmCompanyEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/6/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Crm.DataLayer;

    /// <summary>
    /// Entity to represent content in a web site.
    /// </summary>
    public class MaxCrmCompanyEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxCrmCompanyEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCrmCompanyEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmCompanyEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCrmCompanyEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCrmCompanyDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(typeof(MaxCrmCompanyDataModel)) as MaxCrmCompanyDataModel;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity relationship to the user entity
        /// </summary>
        protected MaxCrmCompanyPersonRelationDataModel DataModelRelationPerson
        {
            get
            {
                return MaxDataLibrary.GetDataModel(typeof(MaxCrmCompanyPersonRelationDataModel)) as MaxCrmCompanyPersonRelationDataModel;
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxCrmCompanyEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxCrmCompanyEntity class.</returns>
        public static MaxCrmCompanyEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCrmCompanyEntity),
                typeof(MaxCrmCompanyDataModel)) as MaxCrmCompanyEntity;
        }

        public void AddLogEntry(string lsLogEntry, string lsLogType)
        {
            MaxCrmCompanyLogEntryEntity loEntity = MaxCrmCompanyLogEntryEntity.Create();
            loEntity.CompanyId = this.Id;
            loEntity.LogEntry = lsLogEntry;
            loEntity.LogType = lsLogType;
            loEntity.LogDate = DateTime.UtcNow;
            loEntity.Insert();
        }
    }
}
