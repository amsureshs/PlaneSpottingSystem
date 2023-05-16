using System;
namespace Rusada.Web.Models.APIViewModels
{
	public class PageListVM<T>
	{
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}

