// <copyright file="MaxCrmGroupViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="4/4/2015" author="Brian A. Lakstins" description="Initial creation.">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Crm.BusinessLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxCrmGroupViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCrmGroupViewModel> _oSortedList = null;

        private List<MaxCrmPersonViewModel> _oPersonList = null;

        private List<MaxCrmPersonViewModel> _oPersonSelectionList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCrmGroupViewModel class
        /// </summary>
        public MaxCrmGroupViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmGroupViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCrmGroupViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCrmGroupViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCrmGroupViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCrmGroupEntity.Create();
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string GroupType
        {
            get;
            set;
        }

        public string PersonId
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public string From
        {
            get;
            set;
        }

        public List<MaxCrmPersonViewModel> PersonList
        {
            get
            {
                if (null == this._oPersonList)
                {
                    MaxCrmGroupEntity loEntity = this.Entity as MaxCrmGroupEntity;
                    if (null != loEntity)
                    {
                        this._oPersonList = new List<MaxCrmPersonViewModel>();
                        MaxCrmGroupPersonRelationEntity loRelation = MaxCrmGroupPersonRelationEntity.Create();
                        MaxEntityList loList = loRelation.LoadAllByGroupId(loEntity.Id);
                        for (int lnE = 0; lnE < loList.Count; lnE++)
                        {
                            loRelation = loList[lnE] as MaxCrmGroupPersonRelationEntity;
                            MaxCrmPersonViewModel loPerson = new MaxCrmPersonViewModel(loRelation.PersonId.ToString());
                            this._oPersonList.Add(loPerson);
                        }
                    }
                }

                return this._oPersonList;
            }
        }

        public List<MaxCrmPersonViewModel> PersonSelectionList
        {
            get
            {
                if (null == this._oPersonSelectionList)
                {
                    this._oPersonSelectionList = new List<MaxCrmPersonViewModel>();
                    MaxEntityList loList = MaxCrmPersonEntity.Create().LoadAllCache();
                    List<string> loPersonIdList = new List<string>();
                    foreach (MaxCrmPersonViewModel loModel in this.PersonList)
                    {
                        loPersonIdList.Add(loModel.Id);
                    }

                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        MaxCrmPersonEntity loEntity = loList[lnE] as MaxCrmPersonEntity;
                        if (!loPersonIdList.Contains(loEntity.Id.ToString()))
                        {
                            MaxCrmPersonViewModel loPerson = new MaxCrmPersonViewModel(loEntity.Id.ToString());
                            this._oPersonSelectionList.Add(loPerson);
                        }
                    }
                }

                return this._oPersonSelectionList;
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCrmGroupViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCrmGroupViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCrmGroupViewModel loViewModel = new MaxCrmGroupViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public string AddPerson()
        {
            string lsR = "No person selected.";
            if (!string.IsNullOrEmpty(this.PersonId))
            {
                MaxCrmGroupPersonRelationEntity loRelation = MaxCrmGroupPersonRelationEntity.Create();
                Guid loPersonId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.PersonId);
                Guid loGroupId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.Id);
                lsR = "Id not available.";
                if (Guid.Empty != loPersonId && Guid.Empty != loGroupId)
                {
                    loRelation.PersonId = loPersonId;
                    loRelation.GroupId = loGroupId;
                    loRelation.Name = "Crm";
                    loRelation.Insert();
                    lsR = "Person added to group.";
                }
            }

            return lsR;
        }

        public string Send()
        {
            string lsR = string.Empty;
            //MaxFactry.Base.DataLayer.IMaxMessageRepositoryProvider loProvider = MaxFactry.Core.MaxFactryLibrary.CreateProvider(typeof(MaxFactry.General.DataLayer.Provider.MaxMessageRepositoryTwilioProvider)) as MaxFactry.Base.DataLayer.IMaxMessageRepositoryProvider;
            MaxFactry.Base.DataLayer.IMaxMessageRepositoryProvider loProvider = null;
            this.EntityLoad();
            foreach (MaxCrmPersonViewModel loModel in this.PersonList)
            {
                MaxFactry.General.DataLayer.MaxEmailDataModel loDataModel = new MaxFactry.General.DataLayer.MaxEmailDataModel();
                MaxFactry.Base.DataLayer.MaxData loData = new MaxFactry.Base.DataLayer.MaxData(loDataModel);
                loData.Set(loDataModel.FromAddress, this.From);
                loData.Set(loDataModel.ToAddressList, loModel.MainPhone);
                loData.Set(loDataModel.Subject, this.Message);
                loData = loProvider.Send(loData);
                lsR += loData.Get(loDataModel.SendLog).ToString() + "\r\n";
            }

            return lsR;
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
                MaxCrmGroupEntity loEntity = this.Entity as MaxCrmGroupEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    loEntity.GroupType = this.GroupType;
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
                MaxCrmGroupEntity loEntity = this.Entity as MaxCrmGroupEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.GroupType = loEntity.GroupType;
                    return true;
                }
            }

            return false;
        }
    }
}
