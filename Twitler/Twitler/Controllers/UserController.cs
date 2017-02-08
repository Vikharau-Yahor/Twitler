using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Mappers.Mappers;
using Twitler.Utils.Comparers;
using Twitler.Utils.HashTools;
using Twitler.ViewModels.Twit;

namespace Twitler.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITwitRepository _twitRepository;
        private readonly IUserRepository _userRepository;
        private readonly TwitMapper _twitMapper;
        private readonly IHashExtractor _hashExtractor;

        public UserController(ITwitRepository twitRepository,
                              IUserRepository userRepository,
                              TwitMapper twitMapper,
                              IHashExtractor hashExtractor)
        {
            _twitRepository = twitRepository;
            _userRepository = userRepository;
            _twitMapper = twitMapper;
            _hashExtractor = hashExtractor;
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult PostsFeed()
        {
            var twits = _twitRepository.GetAll();
            var twitsViewModels = twits.Select(_twitMapper.ToTwitVm)
                .OrderByDescending(t => t.DatePost).ToList();

            return PartialView("_PartialMessages", twitsViewModels);
        }

        
        public ActionResult SearchTwits(string searchString)
        {
            var hashValues = _hashExtractor.GetFromString(searchString);       
            var twits = _twitRepository.GetByHashTags(hashValues)
                .OrderBy(t => t, new TwitComparerByHashTag(hashValues));
            var twitsViewModels = twits.Select(_twitMapper.ToTwitVm).ToList();
            return PartialView("_PartialMessages", twitsViewModels);
        }

        [HttpPost]
        public ActionResult PostTwit(PostedTwitJm twitModel)
        {
            if (ModelState.IsValid)
            {
                //var hashValues = _hashExtractor.GetFromString(twitModel.Message);
                var curUser = _userRepository.Get(User.Identity.Name);
                var newTwit = _twitMapper.ToDomainModel(twitModel, curUser);
                _twitRepository.Add(newTwit);

                return RedirectToAction("PostsFeed");
            }
            return new EmptyResult();
        }
    }
}