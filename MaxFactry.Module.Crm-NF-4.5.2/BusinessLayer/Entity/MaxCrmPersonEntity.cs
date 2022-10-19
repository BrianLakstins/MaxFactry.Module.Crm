// <copyright file="MaxCrmPersonEntity.cs" company="Lakstins Family, LLC">
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
    public class MaxCrmPersonEntity : MaxFactry.Base.BusinessLayer.MaxBasePersonEntity
    {
        private MaxCrmAddressEntity _oBillAddress = null;

        private MaxCrmAddressEntity _oShipAddress = null;

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCrmPersonEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCrmPersonEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCrmPersonEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string MainPhone
        {
            get
            {
                return this.GetString(this.DataModel.MainPhone);
            }

            set
            {
                this.Set(this.DataModel.MainPhone, value);
            }
        }

        public string MainEmail
        {
            get
            {
                return this.GetString(this.DataModel.MainEmail);
            }

            set
            {
                this.Set(this.DataModel.MainEmail, value);
            }
        }

        public string SourceType
        {
            get
            {
                return this.GetString(this.DataModel.SourceType);
            }

            set
            {
                this.Set(this.DataModel.SourceType, value);
            }
        }

        public string SourceId
        {
            get
            {
                return this.GetString(this.DataModel.SourceId);
            }

            set
            {
                this.Set(this.DataModel.SourceId, value);
            }
        }

        public DateTime SourceDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.SourceDate);
            }

            set
            {
                this.Set(this.DataModel.SourceDate, value);
            }
        }

        public MaxCrmAddressEntity BillAddress
        {
            get
            {
                if (null == this._oBillAddress)
                {
                    MaxCrmPersonAddressRelationEntity loRelation = MaxCrmPersonAddressRelationEntity.Create();
                    MaxEntityList loRelationList = loRelation.LoadAllByPersonId(this.Id);
                    this._oBillAddress = MaxCrmAddressEntity.Create();
                    for (int lnE = 0; lnE < loRelationList.Count; lnE++)
                    {
                        if (((MaxCrmPersonAddressRelationEntity)loRelationList[lnE]).RelationType == "BillAddress")
                        {
                            if (((MaxCrmPersonAddressRelationEntity)loRelationList[lnE]).EndDate > loRelation.EndDate)
                            {
                                loRelation = (MaxCrmPersonAddressRelationEntity)loRelationList[lnE];
                            }
                        }
                    }

                    if (!Guid.Empty.Equals(loRelation.AddressId))
                    {
                        this._oBillAddress.LoadByIdCache(loRelation.AddressId);
                    }
                }

                return this._oBillAddress;
            }
        }

        public MaxCrmAddressEntity ShipAddress
        {
            get
            {
                if (null == this._oShipAddress)
                {
                    MaxCrmPersonAddressRelationEntity loRelation = MaxCrmPersonAddressRelationEntity.Create();
                    MaxEntityList loRelationList = loRelation.LoadAllByPersonId(this.Id);
                    this._oShipAddress = MaxCrmAddressEntity.Create();
                    for (int lnE = 0; lnE < loRelationList.Count; lnE++)
                    {
                        if (((MaxCrmPersonAddressRelationEntity)loRelationList[lnE]).RelationType == "ShipAddress")
                        {
                            if (((MaxCrmPersonAddressRelationEntity)loRelationList[lnE]).EndDate > loRelation.EndDate)
                            {
                                loRelation = (MaxCrmPersonAddressRelationEntity)loRelationList[lnE];
                            }
                        }
                    }

                    if (!Guid.Empty.Equals(loRelation.AddressId))
                    {
                        this._oShipAddress.LoadByIdCache(loRelation.AddressId);
                    }
                }

                return this._oShipAddress;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCrmPersonDataModel DataModel
        {
            get
            {
                MaxDataModel loDataModel = MaxDataLibrary.GetDataModel(typeof(MaxCrmPersonDataModel));
                if (loDataModel is MaxCrmPersonDataModel)
                {
                    return (MaxCrmPersonDataModel)loDataModel;
                }

                return null;
            }
        }

        /// <summary>
        /// Creates a new instance of the MaxCrmPersonEntity class.
        /// </summary>
        /// <returns>a new instance of the MaxCrmPersonEntity class.</returns>
        public static MaxCrmPersonEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCrmPersonEntity),
                typeof(MaxCrmPersonDataModel)) as MaxCrmPersonEntity;
        }

        public void AddLogEntry(string lsLogEntry, string lsLogType)
        {
            MaxCrmPersonLogEntryEntity loEntity = MaxCrmPersonLogEntryEntity.Create();
            loEntity.PersonId = this.Id;
            loEntity.LogEntry = lsLogEntry;
            loEntity.LogType = lsLogType;
            loEntity.LogDate = DateTime.UtcNow;
            loEntity.Insert();
        }


        public virtual MaxEntityList LoadAllBySource(string lsSourceType, string lsSourceId)
        {
            MaxDataList loDataList = MaxCrmRepository.SelectAllByProperty(this.Data, this.DataModel.SourceId, lsSourceId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType());
            for (int lnD = 0; lnD < loDataList.Count; lnD++)
            {
                MaxCrmPersonEntity loEntity = MaxCrmPersonEntity.Create();
                loEntity.Load(loDataList[lnD]);
                if (loEntity.SourceType.Equals(lsSourceType, StringComparison.InvariantCultureIgnoreCase))
                {
                    loEntityList.Add(loEntity);
                }
            }

            return loEntityList;
        }

        public virtual MaxEntityList LoadAllByKeyFields(string lsFirstName, string lsLastName, string lsEmail)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loAllList = this.LoadAllCache();
            for (int lnE = 0; lnE < loAllList.Count; lnE++)
            {
                MaxCrmPersonEntity loEntity = loAllList[lnE] as MaxCrmPersonEntity;
                if (loEntity.CurrentFirstName.Equals(lsFirstName, StringComparison.InvariantCultureIgnoreCase) &&
                    loEntity.CurrentLastName.Equals(lsLastName, StringComparison.InvariantCultureIgnoreCase) &&
                    loEntity.MainEmail.Equals(lsEmail, StringComparison.InvariantCultureIgnoreCase))
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }
    }
}
