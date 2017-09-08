using Sakura.AspNetCore;

namespace ViewModels.Common
{
    public class PaggerModel
    {
        public PaggerModel(IPagedList source, AjaxOptions ajaxOptins = null)
        {
            Source = source;
            AjaxOptions = ajaxOptins;
        }

        public AjaxOptions AjaxOptions { get; set; }

        public string IsAjaxString => AjaxOptions == null ?"false":"true";

        public IPagedList Source { get; set; }
    }

    public class AjaxOptions
    {
        public AjaxOptions(string updateTag,string searchForm, string method= "Post", string mode = "replace")
        {
            Method = method;
            Mode = mode;
            UpdateTag = updateTag;
            SearchForm = searchForm;
        }
        public string Method { get; set; } 
        public string Mode { get; set; }
        public string UpdateTag { get; set; }
        public string SearchForm { get; set; }
    }
}