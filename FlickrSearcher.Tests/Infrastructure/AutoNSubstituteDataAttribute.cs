﻿using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace FlickrSearcher.Tests.Infrastructure
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
