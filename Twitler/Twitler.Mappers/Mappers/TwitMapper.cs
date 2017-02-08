using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Twitler.Domain.Model;
using Twitler.Utils.HashTools;
using Twitler.ViewModels.Twit;

namespace Twitler.Mappers.Mappers
{
    public class TwitMapper
    {
        private IMapper _mapper;
        private IHashExtractor _hashExtractor;

        public TwitMapper(IMapper mapper, IHashExtractor hashExtractor)
        {
            _mapper = mapper;
            _hashExtractor = hashExtractor;
        }

        public Twit ToDomainModel(PostedTwitJm jsonModel, User user)
        {
            var twit = _mapper.Map<PostedTwitJm, Twit>(jsonModel);
            twit.User = user;
            twit.HashTags = _hashExtractor.GetFromString(jsonModel.Message)
                .Select(ht => new HashTag { HashValue = ht }).ToList();
            return twit;
        }

        public TwitVm ToTwitVm(Twit model)
        {
            return _mapper.Map<Twit, TwitVm>(model);
        }
    }
}
