using System.Collections.Generic;
namespace hospital_api.DTO.Response
{
    public class PagedResponse<TModel> : BaseResponse
    {
        const int MaxPageSize = 50;
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
          
        
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int TotalItems { get; set; }
        public string DisplayingText { get; set; }


        public IList<TModel>? Items { get; set; }

        public PagedResponse()
        {
            Items = new List<TModel>();
        }
    }
}

