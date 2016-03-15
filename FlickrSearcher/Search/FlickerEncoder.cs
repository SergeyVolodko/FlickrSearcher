using System;

namespace FlickrSearcher.Search
{
    public interface IFlickerEncoder
    {
        string Encode(long num);
    }


    public class FlickerEncoder: IFlickerEncoder
    {
        protected static string alphabetString = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
        protected static char[] alphabet = alphabetString.ToCharArray();
        protected static int base_count = alphabet.Length;

        public string Encode(long num)
        {
            string result = "";
            long div;
            int mod = 0;

            while (num >= base_count)
            {
                div = num / base_count;
                mod = (int)(num - (base_count * (long)div));
                result = alphabet[mod] + result;
                num = (long)div;
            }
            if (num > 0)
            {
                result = alphabet[(int)num] + result;
            }
            return result;
        }


        //public static long decode(String link)
        //{
        //    long result = 0;
        //    long multi = 1;
        //    while (link.Length > 0)
        //    {
        //        String digit = link.Substring(link.Length - 1);
        //        result = result + multi * alphabetString.LastIndexOf(digit);
        //        multi = multi * base_count;
        //        link = link.Substring(0, link.Length - 1);
        //    }
        //    return result;
        //}
    }
}