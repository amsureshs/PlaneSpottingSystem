using System;
namespace Rusada.Data.Models.DTOs
{
	public class PageListDataDTO<T>
	{
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}

