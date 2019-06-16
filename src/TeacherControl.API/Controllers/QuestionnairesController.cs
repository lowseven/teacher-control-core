﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
    [Route("api/assignments/{assignmentId:int:min(1)}/questionnaires")]
    public class QuestionnairesController : Controller
    {
        protected readonly IQuestionnaireRepository _QuestionnaireRepo;
        protected readonly ILogger<Controller> _Logger;

        public QuestionnairesController(IQuestionnaireRepository questionnaireRepo, ILogger<Controller> logger)
        {
            _QuestionnaireRepo = questionnaireRepo;
            _Logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll([FromRoute] int assignmentId)
        {
            return this.Ok(() =>
                _QuestionnaireRepo.GetAllQuestionnaires(assignmentId).Result, _Logger);
        }

        [HttpPost]
        public IActionResult AddQuestionnaire([FromRoute] int assignmentId, [FromBody] QuestionnaireDTO dto)
        {
            string username = this.GetUsername();
            return this.NoContent(() => _QuestionnaireRepo.Add(assignmentId, dto, username) > 0, _Logger);
        }

        [HttpGet, Route("{questionnaireId:int:min(1)}/questions")]
        public IActionResult GetQuestionnaireQuestions([FromRoute] int assignmentId, [FromRoute] int questionnaireId)
        {
            return this.Ok(() =>
                _QuestionnaireRepo.GetQuestions(assignmentId, questionnaireId), _Logger);
        }

        [HttpGet, Route("{questionnaireId:int:min(1)}/correct-answers")]
        public IActionResult GetQuestionnaireAnswers([FromRoute] int assignmentId, [FromRoute] int questionnaireId)
        {
            return this.Ok(() =>
                _QuestionnaireRepo.GetCorrectQuestionAnswers(assignmentId, questionnaireId), _Logger);
        }

        [HttpGet, Route("{questionnaireId:int:min(1)}/answer-matches")]
        public IActionResult GetQuestionnaireAnswerMatches([FromRoute] int assignmentId, [FromRoute] int questionnaireId)
        {
            return this.Ok(() =>
                _QuestionnaireRepo.GetQuestionAnswerMatches(assignmentId, questionnaireId), _Logger);
        }

        //[HttpGet, Route("{questionnaireId:int:min(1)}/commitments/{commitmentId:int:min(1)}")]
        //public IActionResult GetCommitments([FromRoute] int questionnaireId, [FromRoute] int commitmentId)
        //{
        //    return this.Ok(() => JArray.FromObject(_CommitmentRepo.GetByCommitmentId(questionnaireId, commitmentId)));
        //}

        //[HttpGet, Route("{questionnaireId:int:min(1)}/commitments/{username:length(3, 60)}")] //username length-range
        ////TODO: review if the length is correct
        //public IActionResult GetCommitmentsByUsername([FromRoute] int questionnaireId, [FromRoute] string username)
        //{
        //    return this.Ok(() => JArray.FromObject(_CommitmentRepo.GetByUsername(questionnaireId, username)));
        //}

        //[HttpGet, Route("{questionnaireId:int:min(1)}/commitments/{userId:int:min(1)}/last-commitment")]
        //public IActionResult GetLastCommitments([FromRoute] int questionnaireId, [FromRoute] int userId)
        //{
        //    return this.Ok(() => JArray.FromObject(_CommitmentRepo.GetLastCommitmentByUserId(questionnaireId, userId)));
        //}

        //[HttpPost, Route("{questionnaireId:int:min(1)}/commitments")]
        //public IActionResult AddCommitment([FromRoute] int questionnaireId, [FromBody] CommitmentDTO dto)
        //{
        //    return this.NoContent(() => _CommitmentRepo.Add(questionnaireId, dto, this.GetUsername()) > 0);
        //}
    }
}