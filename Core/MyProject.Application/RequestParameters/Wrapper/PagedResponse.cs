using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.RequestParameters.Wrapper
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {

            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pageSize));
            this.TotalRecords = totalRecords;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
        }
    }
}
