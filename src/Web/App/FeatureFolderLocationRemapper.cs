using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Razor;

namespace AspNet5.Beta6.Examples.FeatureFolders.App
{
    /// <summary>
    ///     Relocates the view folder mapping to "/App/Features/" and renames the "Shared" folder to "_Shared"
    /// </summary>
    /// <remarks>
    ///     Based on this blog post by Jeff Fritz:
    ///     http://www.jeffreyfritz.com/2015/05/customize-asp-net-mvc-6-view-location-easily/
    ///     MVC Feature Folders explained: http://timgthomas.com/2013/10/feature-folders-in-asp-net-mvc/
    ///     Add this code snippet to your services configuration in StartUp.cs:
    ///     <c>
    ///         services.Configure&lt;RazorViewEngineOptions>(o => 
    ///         {
    ///         o.ViewLocationExpanders.Add(new FeatureFolderLocationRemapper());
    ///         });
    ///     </c>
    /// </remarks>
    public class FeatureFolderLocationRemapper : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            return viewLocations.MoveViewsIntoFeaturesFolder().CutomizeSharedWithUnderScore();
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // do nothing.. not entirely needed for this 
        }
    }

    public static class FeatureFolderExtensions
    {
        public static IEnumerable<string> MoveViewsIntoFeaturesFolder(this IEnumerable<string> viewLocations)
        {
            // I added an additional "App" folder in my MVC project to separate the root configuration 
            // and package files from my application code. 
            return viewLocations.Select(f => f.Replace("/Views/", "/App/Features/"));
        }

        public static IEnumerable<string> CutomizeSharedWithUnderScore(this IEnumerable<string> viewLocations)
        {
            return viewLocations.Select(f => f.Replace("/Shared/", "/_Shared/"));
        }
    }
}