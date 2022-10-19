// <copyright file="MaxCrmPersonViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="4/13/2016" author="Brian A. Lakstins" description="Fix for when source type is not set.">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.PresentationLayer
{
	using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Crm.BusinessLayer;

    /// <summary>
    /// View model for managing clients.
    /// </summary>
    public class MaxCrmPersonViewModel : MaxFactry.Base.PresentationLayer.MaxBasePersonViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCrmPersonViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonViewModel class
        /// </summary>
        public MaxCrmPersonViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCrmPersonViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCrmPersonEntity.Create();
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonViewModel class
        /// </summary>
        /// <param name="lsId">Id associated with the entity.</param>
        public MaxCrmPersonViewModel(string lsId)
        {
            this.Entity = MaxCrmPersonEntity.Create();
            this.Id = lsId;
            this.EntityLoad();
            this.Load();
        }

        public string MainPhone
        {
            get;
            set;
        }

        public string MainEmail
        {
            get;
            set;
        }

        public string SourceType
        {
            get;
            set;
        }

        public string SourceId
        {
            get;
            set;
        }

        public DateTime SourceDate
        {
            get;
            set;
        }

        public string NameForList
        {
            get
            {
                string lsR = this.Name;
                if (!string.IsNullOrEmpty(this.MainEmail))
                {
                    lsR += " (" + this.MainEmail + ")";
                }

                if (!string.IsNullOrEmpty(this.SourceType))
                {
                    if (this.SourceType.Equals("MaxCatalogOrder"))
                    {
                        lsR += " OMS";
                    }
                    else if (this.SourceType.Equals("MaxQBCustomer"))
                    {
                        lsR += " QB";
                    }
                }

                return lsR;
            }
        }

        public List<MaxCrmPersonViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCrmPersonViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCrmPersonViewModel loViewModel = new MaxCrmPersonViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        private static List<MaxCrmPersonViewModel> _oList = null;

        private static object _oLock = new object();

        public static List<MaxCrmPersonViewModel> GetContactPersonList()
        {
            if (null == _oList)
            {
                lock (_oLock)
                {
                    if (null == _oList)
                    {
                        _oList = new List<MaxCrmPersonViewModel>();
                        UpdateContactPersonList();
                    }
                }
            }

            return _oList;
        }

        private static void UpdateContactPersonList()
        {
            List<MaxCrmPersonViewModel> loR = new List<MaxCrmPersonViewModel>();
            MaxEntityList loList = MaxCrmPersonEntity.Create().LoadAll();
            SortedList<string, MaxCrmPersonEntity> loSortedList = new SortedList<string, MaxCrmPersonEntity>();

            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxCrmPersonEntity loEntity = loList[lnE] as MaxCrmPersonEntity;
                if (null != loEntity)
                {
                    string lsKey = loEntity.Name;
                    if (!string.IsNullOrEmpty(loEntity.MainEmail))
                    {
                        lsKey += " (" + loEntity.MainEmail + ")";
                    }

                    if (loSortedList.ContainsKey(lsKey))
                    {
                        if (loEntity.SourceDate > loSortedList[lsKey].SourceDate)
                        {
                            loSortedList[lsKey] = loEntity;
                        }
                    }
                    else
                    {
                        loSortedList.Add(lsKey, loEntity);
                    }
                }
            }

            foreach (MaxFactry.Base.BusinessLayer.MaxBasePersonEntity loEntity in loSortedList.Values)
            {
                MaxCrmPersonViewModel loModel = new MaxCrmPersonViewModel(loEntity);
                loModel.Load();
                loR.Add(loModel);
            }

            _oList = loR;
        }

        public static void UpdateCache()
        {
            UpdateContactPersonList();
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
                MaxCrmPersonEntity loEntity = this.Entity as MaxCrmPersonEntity;
                if (null != loEntity)
                {
                    loEntity.SourceType = this.SourceType;
                    loEntity.SourceId = this.SourceId;
                    loEntity.MainEmail = this.MainEmail;
                    loEntity.MainPhone = this.MainPhone;
                    loEntity.SourceDate = this.SourceDate;

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
                MaxCrmPersonEntity loEntity = this.Entity as MaxCrmPersonEntity;
                if (null != loEntity)
                {
                    this.SourceType = loEntity.SourceType;
                    this.SourceId = loEntity.SourceId;
                    this.MainEmail = loEntity.MainEmail;
                    this.MainPhone = loEntity.MainPhone;
                    this.SourceDate = loEntity.SourceDate;
            
                    return true;
                }
            }

            return false;
        }

        public Guid SaveNew()
        {
            MaxCrmPersonEntity loEntity = MaxCrmPersonEntity.Create();
            loEntity.Insert();
            return loEntity.Id;
        }
    }
}
