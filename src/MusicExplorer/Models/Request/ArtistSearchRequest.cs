﻿using MediatR;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Models.Request
{
    public class ArtistSearchRequest : IRequest<ArtistSearchResponse>
    {
        public string SearchCriteria { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
