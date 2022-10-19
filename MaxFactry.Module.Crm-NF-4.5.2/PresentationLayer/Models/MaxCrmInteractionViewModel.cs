// <copyright file="MaxCrmInteractionViewModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Module.Crm.Mvc4.PresentationLayer
{
	using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Crm.BusinessLayer;

	/// <summary>
    /// View model for managing clients.
	/// </summary>
	public class MaxCrmInteractionViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
	{
        /// <summary>
        /// Initializes a new instance of the MaxCrmInteractionViewModel class
        /// </summary>
        public MaxCrmInteractionViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmInteractionViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCrmInteractionViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmInteractionViewModel class
        /// </summary>
        /// <param name="lsId">Id associated with the entity.</param>
        public MaxCrmInteractionViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCrmInteractionEntity.Create();
        }

        public string StartDate
        {
            get;
            set;
        }

        public string EndDate
        {
            get;
            set;
        }

        public string InteractionType
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Address1
        {
            get;
            set;
        }

        public string Address2
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string EquipmentDesired
        {
            get;
            set;
        }

        public string Tractor
        {
            get;
            set;
        }

        public string HorsePower
        {
            get;
            set;
        }

        public string Acreage
        {
            get;
            set;
        }

        public string TypeOfAcreage
        {
            get;
            set;
        }

        public string HowHeard
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }


        /// <summary>
        /// Loads the entity based on the Id property.
        /// Maps the current values of properties in the ViewModel to the Entity.
        /// </summary>
        /// <returns>True if successful. False if it cannot be mapped.</returns>
        protected override bool MapToEntity()
        {
            if (base.MapToEntity())
            {
                MaxCrmInteractionEntity loEntity = this.Entity as MaxCrmInteractionEntity;
                if (null != loEntity)
                {
                    DateTime loStartDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), this.StartDate);
                    if (!loStartDate.Equals(DateTime.MinValue))
                    {
                        loEntity.StartDate = loStartDate;
                    }

                    DateTime loEndDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), this.EndDate);
                    if (!loEndDate.Equals(DateTime.MinValue))
                    {
                        loEntity.EndDate = loEndDate;
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the properties of the Entity to the properties of the ViewModel.
        /// </summary>
        /// <returns>True if the entity exists.</returns>
        protected override bool MapFromEntity()
        {
            if (base.MapFromEntity())
            {
                MaxCrmInteractionEntity loEntity = this.Entity as MaxCrmInteractionEntity;
                if (null != loEntity)
                {
                    this.StartDate = MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.StartDate).ToString();
                    if (loEntity.StartDate.Equals(DateTime.MinValue) && !loEntity.Id.Equals(Guid.Empty))
                    {
                        this.StartDate = MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.CreatedDate).ToString();
                    }

                    this.EndDate = MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.EndDate).ToString();
                    if (loEntity.EndDate.Equals(DateTime.MinValue))
                    {
                        this.EndDate = string.Empty;
                    }        
            
                    return true;
                }
            }

            return false;
        }

        public Guid SaveNew()
        {
            MaxCrmInteractionEntity loEntity = MaxCrmInteractionEntity.Create();
            loEntity.Insert();
            return loEntity.Id;
        }
    }
}
