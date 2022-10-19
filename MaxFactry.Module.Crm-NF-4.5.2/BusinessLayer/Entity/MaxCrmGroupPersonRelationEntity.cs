// <copyright file="MaxCrmGroupPersonRelationEntity.cs" GroupPersonRelation="Lakstins Family, LLC">
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
    public class MaxCrmGroupPersonRelationEntity : MaxFactry.Base.BusinessLayer.MaxRelationEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxCrmGroupPersonRelationEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCrmGroupPersonRelationEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmGroupPersonRelationEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCrmGroupPersonRelationEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid GroupId
        {
            get
            {
                return this.ParentId;
            }

            set
            {
                this.ParentId = value;
            }
        }

        public Guid PersonId
        {
            get
            {
                return this.ChildId;
            }

            set
            {
                this.ChildId = value;
            }
        }

        public string Description
        {
            get
            {
                return this.GetString(this.DataModel.Description);
            }

            set
            {
                this.Set(this.DataModel.Description, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCrmGroupPersonRelationDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(typeof(MaxCrmGroupPersonRelationDataModel)) as MaxCrmGroupPersonRelationDataModel;
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxCrmGroupPersonRelationEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxCrmGroupPersonRelationEntity class.</returns>
        public static MaxCrmGroupPersonRelationEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCrmGroupPersonRelationEntity),
                typeof(MaxCrmGroupPersonRelationDataModel)) as MaxCrmGroupPersonRelationEntity;
        }

        /// <summary>
        /// Loads all entities that match the given Person Id
        /// </summary>
        /// <param name="loPersonId">The Id of the person to load companies for.</param>
        /// <returns>List of users.</returns>
        public MaxEntityList LoadAllByGroupId(Guid loGroupId)
        {
            return this.LoadAllByParentId(loGroupId);
        }

        /// <summary>
        /// Loads all entities that match the given Address Id
        /// </summary>
        /// <param name="loPersonId">The Id of the address to load persons for.</param>
        /// <returns>List of users.</returns>
        public MaxEntityList LoadAllByPersonId(Guid loPersonId)
        {
            return this.LoadAllByChildId(loPersonId);
        }
    }
}
