using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Filters;
using Twitler.Mappers.Mappers;
using Twitler.Services.Queries;
using Twitler.Utils.Comparers;
using Twitler.Utils.HashTools;
using Twitler.ViewModels.Twit;

namespace Twitler.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IRepository<Twit> _twitRepository;
        private readonly IRepository<User> _userRepository;
        private readonly TwitMapper _twitMapper;
        private readonly IHashExtractor _hashExtractor;

        public UserController(IRepository<Twit> twitRepository,
            IRepository<User> userRepository,
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
            var curUserName = User.Identity.Name;
            var twits = _twitRepository.Get(new TwitAllQuery()).ToList();
            var twitsViewModels = twits.Select(t => _twitMapper.ToTwitVm(t, curUserName))
                .OrderByDescending(t => t.DatePost).ToList();

            return PartialView("_PartialMessages", twitsViewModels);
        }


        public ActionResult SearchTwits(string searchString)
        {
            var curUserName = User.Identity.Name;
            var hashValues = _hashExtractor.GetFromString(searchString);
            var query = new TwitByHashTagsQuery(hashValues);
            var twits = _twitRepository.Get(query).ToList()
                .OrderBy(t => t, new TwitComparerByHashTag(hashValues));
            var twitsViewModels = twits.Select(t => _twitMapper.ToTwitVm(t, curUserName)).ToList();
            return PartialView("_PartialMessages", twitsViewModels);
        }

        [HttpPost]
        public ActionResult PostTwit(PostedTwitJm twitModel)
        {
            if (ModelState.IsValid)
            {
                var curUser = _userRepository.Get(new UserByEmailQuery(User.Identity.Name))
                    .SingleOrDefault();
                var newTwit = _twitMapper.ToDomainModel(twitModel, curUser);
                _twitRepository.Add(newTwit);

                return RedirectToAction("PostsFeed");
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteTwit(int twitId)
        {
            var query = new TwitByUserQuery(User.Identity.Name, twitId);
            var removedTwit = _twitRepository.Get(query).SingleOrDefault();
            _twitRepository.Delete(removedTwit);

            return RedirectToAction("PostsFeed");
        }
    }
}