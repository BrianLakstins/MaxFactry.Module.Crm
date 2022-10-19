// <copyright file="MaxCrmPersonLogEntryEntity.cs" company="Lakstins Family, LLC">
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
    public class MaxCrmPersonLogEntryEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonLogEntryEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCrmPersonLogEntryEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonLogEntryEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCrmPersonLogEntryEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }


        public Guid PersonId
        {
            get
            {
                return this.GetGuid(this.DataModel.PersonId);
            }

            set
            {
                this.Set(this.DataModel.PersonId, value);
            }
        }

        public DateTime LogDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.LogDate);
            }

            set
            {
                this.Set(this.DataModel.LogDate, value);
            }
        }

        public string LogEntry
        {
            get
            {
                return this.GetString(this.DataModel.LogEntry);
            }

            set
            {
                this.Set(this.DataModel.LogEntry, value);
            }
        }

        public string LogType
        {
            get
            {
                return this.GetString(this.DataModel.LogType);
            }

            set
            {
                this.Set(this.DataModel.LogType, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCrmPersonLogEntryDataModel DataModel
        {
            get
            {
                MaxDataModel loDataModel = MaxDataLibrary.GetDataModel(typeof(MaxCrmPersonLogEntryDataModel));
                if (loDataModel is MaxCrmPersonLogEntryDataModel)
                {
                    return (MaxCrmPersonLogEntryDataModel)loDataModel;
                }

                return null;
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxCrmPersonLogEntryEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxCrmPersonLogEntryEntity class.</returns>
        public static MaxCrmPersonLogEntryEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCrmPersonLogEntryEntity),
                typeof(MaxCrmPersonLogEntryDataModel)) as MaxCrmPersonLogEntryEntity;
        }
    }
}
