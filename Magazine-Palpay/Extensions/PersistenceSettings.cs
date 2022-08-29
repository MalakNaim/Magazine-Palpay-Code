// --------------------------------------------------------------------------------------------------
// <copyright file="PersistenceSettings.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace Magazine_Palpay.Web.Settings
{
    public class PersistenceSettings
    {
        public bool UseOracle { get; set; }

        public bool UseMsSql { get; set; }

        public bool UsePostgres { get; set; }

        public PersistenceConnectionStrings ConnectionStrings { get; set; }

        public class PersistenceConnectionStrings
        {
            // ReSharper disable once InconsistentNaming
            public string MSSQL { get; set; }

            public string Postgres { get; set; }

            public string Oracle { get; set; }
        }
    }
}