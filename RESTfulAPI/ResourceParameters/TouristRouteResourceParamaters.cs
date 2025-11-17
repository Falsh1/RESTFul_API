using System.Text.RegularExpressions;

namespace RESTfulAPI.ResourceParameters
{
    public class TouristRouteResourceParamaters
    {
        public string Keyword { get; set; }

        public string? operatorType { get; set; }
        public int? ratingValue { get; set; }

        //包装器属性（wrapper property）
        private string _rating { get; set;}
        public string? Rating { 
            get{ return _rating; }
            set
            {
                _rating = value;
                //解析rating参数
                if (!string.IsNullOrWhiteSpace(_rating))
                {
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    Match match = regex.Match(_rating);
                    if (match.Success)
                    {
                        operatorType = match.Groups[1].Value;
                        ratingValue = int.Parse(match.Groups[2].Value);
                    }
                }
            }
        }
    }
}
