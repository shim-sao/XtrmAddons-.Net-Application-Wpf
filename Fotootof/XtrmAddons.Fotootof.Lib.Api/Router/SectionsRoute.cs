using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.Api.Router
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Api Router Sections.
    /// </summary>
    public class SectionsRoute : Router
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router Sections.
        /// </summary>
        public SectionsRoute() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router Sections.
        /// </summary>
        /// <param name="uri">The Uri to parse.</param>
        public SectionsRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to get a list of associated Sections of an User.
        /// </summary>
        /// <returns>WPF Web Server response data of list of Sections.</returns>
        public override WebServerResponseData Index()
        {
            log.Info("Api : Serving sections request. Please wait...");
            
            if (!IsAuth())
            {
                log.Info("Api : Return response user not auth.");
                return ResponseNotAuth();
            }
            
            try
            {
                // Get user and dependencies.
                UserEntity user = GetAuthUser();

                List<SectionEntity> l = new List<SectionEntity>();

                foreach (AclGroupEntity group in user.AclGroups)
                {
                    AclGroupEntity entity = Database.AclGroups.SingleOrDefault
                        (
                            new AclGroupOptionsSelect

                            {
                                PrimaryKey = group.PrimaryKey,
                                Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                            }
                        );

                    /*
                    foreach (Section sectionEntity in entity.Sections)
                    {
                        l.Add(
                            Database.Section_SingleOrNull
                            (
                                new SectionOptionsSelect
                                {
                                    PrimaryKey = section.PrimaryKey,
                                    Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                                }
                            )
                        );
                    }
                    */
                }

                // Get list of folders associated to user.
                Content["Authentication"] = true;
                Content["Response"] = ConvertJsonAuthSections(l);
                return ResponseContentToJson();
            }
            catch (Exception e)
            {
                log.Fatal("Api list sections failed", e);
                return ResponseError500();
            }
        }

        /// <summary>
        ///  Method to get an associated section.
        /// </summary>
        /// <returns>>WPF Web Server response data of the selected folder.</returns>
        public WebServerResponseData Get()
        {
            log.Info("Api : Serving section request. Please wait !");

            if (!IsAuth())
            {
                log.Info("Api : Return response user not auth.");
                return ResponseNotAuth();
            }
            
            try
            {
                // Check if request params are correct.
                if(Uri.Params.Length == 0)
                {
                    log.Info("Api : Params in the request are empty.");
                    log.Info("Api : Return response section not found or doesn't exists.");
                    return ResponseError404("Section not found or doesn't exists.");
                }

                // Get user and dependencies.
                UserEntity user = GetAuthUser();
                SectionEntity entity = null;

                // Try to found section in dependencies.
                foreach (AclGroupEntity group in user.AclGroups)
                {
                    AclGroupEntity ag = Database.AclGroups.SingleOrDefault
                        (
                            new AclGroupOptionsSelect
                            {
                                PrimaryKey = group.PrimaryKey,
                                Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                            }
                        );

                    entity = ag.Sections.Find(x => x.PrimaryKey == int.Parse(Uri.Params[0]));
                }

                // Check if folder is associated or not.
                if (entity == null)
                {
                    log.Info("Api : Section not found in user association.");
                    log.Info("Api : Return response Section not found or doesn't exists.");
                    return ResponseError404("Section not found or doesn't exists.");
                }

                // Ensure to select dependencies.
                log.Info("Api : Creating section informations. Please wait...");
                entity = Database.Sections.SingleOrDefault
                    (
                        new SectionOptionsSelect
                        {
                            PrimaryKey = entity.PrimaryKey,
                            Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                        }
                    );

                Content["Authentication"] = true;
                Content["Response"] = ConvertJsonAuthSection(entity);
                return ResponseContentToJson();
            }
            catch (Exception e)
            {
                log.Fatal("Api Error : Serving section request failed.", e);
                return ResponseError500();
            }
        }
    }
}