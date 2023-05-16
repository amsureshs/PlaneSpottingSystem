using System;
namespace Rusada.Domain.Models.DTOs
{
	public class PageListDomainDTO<T>
	{
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}

