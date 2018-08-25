using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;

// Les informations générales relatives à un assembly dépendent de
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AssemblyTitle("Fotootof")]
[assembly: AssemblyDescription("Photo albums manager Fotootof.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("www.xtrmaddons.com")]
[assembly: AssemblyProduct("XtrmAddons Fotootof")]
[assembly: AssemblyCopyright("Copyright ©  2017 - 2018")]
[assembly: AssemblyTrademark("XtrmAddons")]
[assembly: AssemblyCulture("")]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("CE543B6B-037D-4D81-995E-A237D9118BF2")]

//Pour commencer à générer des applications localisables, définissez
//<UICulture>CultureUtiliséePourCoder</UICulture> dans votre fichier .csproj
//dans <PropertyGroup>.  Par exemple, si vous utilisez le français
//dans vos fichiers sources, définissez <UICulture> à fr-FR. Puis, supprimez les marques de commentaire de
//l'attribut NeutralResourceLanguage ci-dessous. Mettez à jour "fr-FR" dans
//la ligne ci-après pour qu'elle corresponde au paramètre UICulture du fichier projet.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]
[assembly: NeutralResourcesLanguage("")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //où se trouvent les dictionnaires de ressources spécifiques à un thème
                                     //(utilisé si une ressource est introuvable dans la page,
                                     // ou dictionnaires de ressources de l'application)
    ResourceDictionaryLocation.SourceAssembly //où se trouve le dictionnaire de ressources générique
                                              //(utilisé si une ressource est introuvable dans la page,
                                              // dans l'application ou dans l'un des dictionnaires de ressources spécifiques à un thème)
)]


// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de build et de révision par défaut
// en utilisant '*', comme indiqué ci-dessous :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.18237.1922")]
[assembly: AssemblyFileVersion("1.0.18237.1922")]
