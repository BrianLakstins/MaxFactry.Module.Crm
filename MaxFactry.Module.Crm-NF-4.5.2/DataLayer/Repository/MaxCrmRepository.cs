// <copyright file="MaxCmsRepository.cs" company="Lakstins Family, LLC">
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
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated for consistency of data filter.">
// </changelog>
#endregion

namespace MaxFactry.Module.Crm.DataLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Repository for web content
    /// </summary>
    public class MaxCrmRepository : MaxFactry.Base.DataLayer.MaxBaseIdRepository
    {
        /// <summary>
        /// Selects all not marked as deleted based on a single property
        /// </summary>
        /// <param name="loData">Element with data used to determine the provider.</param>
        /// <param name="lsPropertyName">The name of the property used to select.</param>
        /// <param name="loPropertyValue">The value of the property used to select.</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllByKeyFields(MaxData loData, string lsFirstName, string lsLastName, string lsEmail, params string[] laFields)
        {
            MaxCrmPersonDataModel loDataModel = loData.DataModel as MaxCrmPersonDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            loData.Set(loDataModel.CurrentFirstName, lsFirstName);
            loData.Set(loDataModel.CurrentLastName, lsLastName);
            loData.Set(loDataModel.MainEmail, lsEmail);
            MaxData loDataFilter = new MaxData(loData);
            loDataFilter.Set(loDataModel.CurrentFirstName, lsFirstName);
            loDataFilter.Set(loDataModel.CurrentLastName, lsLastName);
            loDataFilter.Set(loDataModel.MainEmail, lsEmail);
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.CurrentFirstName, "=", lsFirstName);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.CurrentLastName, "=", lsLastName);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.MainEmail, "=", lsEmail);
            loDataQuery.EndGroup();

            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal, laFields);
            return loDataList;
        }
    }
}
