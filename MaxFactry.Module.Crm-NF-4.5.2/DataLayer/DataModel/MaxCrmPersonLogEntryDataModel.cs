// <copyright file="MaxCrmPersonLogEntryDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Module.Crm.DataLayer
{
	using System;
	using MaxFactry.Core;
	using MaxFactry.Base.DataLayer;

	/// <summary>
    /// Data model for the information associated with a person.
	/// </summary>
    public class MaxCrmPersonLogEntryDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
	{
        /// <summary>
        /// Id of Person
        /// </summary>
        public string PersonId = "PersonId";
        
        /// <summary>
        /// Date and time the interaction started.
        /// </summary>
        public string LogDate = "LogDate";

        /// <summary>
        /// Date and time the interaction ended.
        /// </summary>
        public string LogEntry = "LogEntry";

        /// <summary>
        /// Type of interaction.
        /// </summary>
        public string LogType = "LogType";

        /// <summary>
        /// Initializes a new instance of the MaxCrmInteractionDataModel class.
		/// </summary>
        public MaxCrmPersonLogEntryDataModel()
            : base()
		{
            this.RepositoryProviderType = typeof(MaxFactry.Module.Crm.DataLayer.Provider.MaxCrmRepositoryProvider);
            this.RepositoryType = typeof(MaxCrmRepository);
            this.SetDataStorageName("MaxCrmPersonLogEntry");
            this.AddType(this.PersonId, typeof(Guid));
            this.AddType(this.LogDate, typeof(DateTime));
            this.AddType(this.LogEntry, typeof(string));
            this.AddNullable(this.LogType, typeof(string));
        }
	}
}
