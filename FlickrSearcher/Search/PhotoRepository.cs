using System;
using System.Collections.Generic;

namespace FlickrSearcher.Search
{
    public interface IPhotoRepository
    {
        IList<PhotoInfo> Search(string text, int page);
    }

    public class PhotoRepository: IPhotoRepository
    {
        public IList<PhotoInfo> Search(string text, int page)
        {
            throw new NotImplementedException();
        }
    }
}