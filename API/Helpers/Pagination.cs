﻿namespace API.Helpers
{
    public class Pagination<T> where T:class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageIndex,int pageSize,int count,IReadOnlyList<T> data) 
        {
            PageSize = pageSize;
            PageIndex = pageIndex;  
            Count = count;
            Data = data;
        }
    }
}
