﻿// <copyright file="MaxCrmInteractionEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/4/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Crm.DataLayer;

    /// <summary>
    /// Entity to represent an Interaction with a Customer
    /// </summary>
    public class MaxCrmInteractionEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxBaseTemplateEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCrmInteractionEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxBaseTemplateEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCrmInteractionEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public DateTime StartDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.StartDate);
            }

            set
            {
                this.Set(this.DataModel.StartDate, value);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.EndDate);
            }

            set
            {
                this.Set(this.DataModel.EndDate, value);
            }
        }

        public string InteractionType
        {
            get
            {
                return this.GetString(this.DataModel.InteractionType);
            }

            set
            {
                this.Set(this.DataModel.InteractionType, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCrmInteractionDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(this.DataModelType) as MaxCrmInteractionDataModel;
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxCrmInteractionEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxCrmInteractionEntity class.</returns>
        public static MaxCrmInteractionEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCrmInteractionEntity), 
                typeof(MaxCrmInteractionDataModel)) as MaxCrmInteractionEntity;
        }
    }
}
