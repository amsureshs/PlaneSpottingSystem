using System;
namespace Rusada.Data.Models.Entities
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }

		public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}

