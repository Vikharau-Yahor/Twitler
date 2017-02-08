using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Utils.HashTools;
using Twitler.ViewModels.Twit;

namespace Twitler.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITwitRepository _twitRepository;
        private readonly IHashExtractor _hashExtractor;

        public UserController(IMapper mapper,
                              ITwitRepository twitRepository,
                              IHashExtractor hashExtractor)
        {
            _mapper = mapper;
            _twitRepository = twitRepository;
            _hashExtractor = hashExtractor;
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult PostsFeed()
        {
            var twits = _twitRepository.GetAll();
            var twitsViewModels = twits.Select(_mapper.Map<Twit, TwitVm>)
                .OrderByDescending(t => t.DatePost).ToList();

            return PartialView("_PartialMessages", twitsViewModels);
        }

        [HttpPost]
        public ActionResult PostTwit(PostedTwitJm twitModel)
        {
            if (ModelState.IsValid)
            {
                var result = _hashExtractor.GetFromString(twitModel.Message);
                var newTwit = _mapper.Map<PostedTwitJm, Twit>(twitModel);
                _twitRepository.Add(User.Identity.Name, newTwit);

                return RedirectToAction("PostsFeed");
            }
            return new EmptyResult();
        }
    }
}