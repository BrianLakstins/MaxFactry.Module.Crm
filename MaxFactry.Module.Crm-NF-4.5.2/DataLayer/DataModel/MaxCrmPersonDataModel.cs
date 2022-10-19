// <copyright file="MaxCrmPersonDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Module.Crm.DataLayer
{
	using System;
	using MaxFactry.Core;
	using MaxFactry.Base.DataLayer;

	/// <summary>
    /// Data model for the information associated with a person.
	/// </summary>
    public class MaxCrmPersonDataModel : MaxFactry.Base.DataLayer.MaxBasePersonDataModel
	{

        /// <summary>
        /// Main phone number for the person
        /// </summary>
        public readonly string MainPhone = "MainPhone";

        /// <summary>
        /// Main email address for the person
        /// </summary>
        public readonly string MainEmail = "MainEmail";

        /// <summary>
        /// Name of the source of this person
        /// </summary>
        public readonly string SourceType = "SourceType";

        /// <summary>
        /// Id of the source of this person
        /// </summary>
        public readonly string SourceId = "SourceId";

        /// <summary>
        /// Date of the source of this person
        /// </summary>
        public readonly string SourceDate = "SourceDate";

        /// <summary>
        /// Initializes a new instance of the MaxCustomerPersonDataModel class.
		/// </summary>
        public MaxCrmPersonDataModel()
            : base()
		{
            this.RepositoryProviderType = typeof(MaxFactry.Module.Crm.DataLayer.Provider.MaxCrmRepositoryProvider);
            this.RepositoryType = typeof(MaxCrmRepository);
            this.SetDataStorageName("MaxCrmPerson");
            this.AddNullable(this.MainPhone, typeof(MaxShortString));
            this.AddNullable(this.MainEmail, typeof(MaxShortString));
            this.AddNullable(this.SourceType, typeof(MaxShortString));
            this.AddNullable(this.SourceId, typeof(MaxShortString));
            this.AddNullable(this.SourceDate, typeof(DateTime));
        }
	}
}
