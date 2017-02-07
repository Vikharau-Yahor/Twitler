using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.ViewModels.Twit;

namespace Twitler.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITwitRepository _twitRepository;

        public UserController(IMapper mapper,
                              ITwitRepository twitRepository)
        {
            _mapper = mapper;
            _twitRepository = twitRepository;
        }

        public ActionResult Main()
        {
            var twits = _twitRepository.GetAll();
            var twitsViewModels = twits.Select(_mapper.Map<Twit, TwitVm>).ToList();

            return View(twitsViewModels);
        }
    }
}