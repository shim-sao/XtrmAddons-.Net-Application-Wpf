﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fotootof.SQLite.EntityManager.Properties {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Translations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Translations() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Fotootof.SQLite.EntityManager.Properties.Translations", typeof(Translations).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Administrator.
        /// </summary>
        public static string Administrator {
            get {
                return ResourceManager.GetString("Administrator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Group authorized to manage the administration of the server..
        /// </summary>
        public static string AdministratorComment {
            get {
                return ResourceManager.GetString("AdministratorComment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à BEGIN TRANSACTION;
        ///CREATE TABLE IF NOT EXISTS `Versions` (
        ///	`VersionId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
        ///	`AssemblyVersionMin`	TEXT,
        ///	`AssemblyVersionMax`	TEXT,
        ///	`Comment`	TEXT
        ///);
        ///CREATE TABLE IF NOT EXISTS `UsersInAclGroups` (
        ///	`UserId`	INTEGER NOT NULL,
        ///	`AclGroupId`	INTEGER NOT NULL,
        ///	CONSTRAINT `PK_UsersInAclGroups` PRIMARY KEY(`UserId`,`AclGroupId`),
        ///	CONSTRAINT `FK_UsersInAclGroups_AclGroups_AclGroupId` FOREIGN KEY(`AclGroupId`) REFERENCES `AclGroups`(`AclGroupId`) ON DELETE CASCADE,
        ///	CONST [le reste de la chaîne a été tronqué]&quot;;.
        /// </summary>
        public static string SrcSQLiteDatabaseSchema {
            get {
                return ResourceManager.GetString("SrcSQLiteDatabaseSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Visitor.
        /// </summary>
        public static string Visitor {
            get {
                return ResourceManager.GetString("Visitor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Group authorized to visit the library of images..
        /// </summary>
        public static string VisitorComment {
            get {
                return ResourceManager.GetString("VisitorComment", resourceCulture);
            }
        }
    }
}
