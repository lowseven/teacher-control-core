﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        int Add(AssignmentDTO dto);
        int DeleteById(int ID);
        int DeleteByTokenId(string tokenID);
        int Update(int id, AssignmentDTO dto);
        int AddTags(int assignmentId, IEnumerable<string> tags);
        int UpdateTags(int id, IEnumerable<string> tags);
        int ReplaceTags(int id, IEnumerable<string> tags);
        AssignmentDTO GetById(int assignmentId);

        IEnumerable<AssignmentDTO> GetByFilters(AssignmentQuery dto);
        IEnumerable<AssignmentResultDTO> GetQuestionnaireResults(int Id, string username);

        int AddComment(int assignmentId, AssignmentCommentDTO dto);
        int UpdateComment(int assignmentId, int commentId, AssignmentCommentDTO dto);
        int RemoveAssignmentComment(int assignmentId, int commentId);
        int DisableAssignmentComment(int assignmentId, int CommentId);
        int DownvoteComment(int AssingmentId, int CommentId);
        int UpvoteComment(int AssingmentId, int CommentId);
        IEnumerable<AssignmentCommentDTO> GetAllAssignmentComments(int assignmentId, AssignmentCommentQuery Query);
        AssignmentCommentDTO GetAssignmentCommentByCommentId(int assignmentId, int commentId);
    }
}
