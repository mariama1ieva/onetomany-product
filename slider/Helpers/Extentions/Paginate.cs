namespace slider.Helpers.Extentions
{
    public class Paginate<T>
    {
        public List<T> Datas { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }

        public Paginate(List<T> datas, int totalPage, int currentPagde)
        {
            Datas = datas;
            TotalPage = totalPage;
            CurrentPage = currentPagde;
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPage;
            }
        }

        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
    }
}
