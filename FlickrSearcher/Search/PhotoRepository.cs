using System;
using System.Collections.Generic;

namespace FlickrSearcher.Search
{
    public interface IPhotoRepository
    {
        IList<FoundPhoto> Search(string text, int page);
    }

    public class PhotoRepository: IPhotoRepository
    {
        public IList<FoundPhoto> Search(string text, int page)
        {
            throw new NotImplementedException();
        }
    }
}