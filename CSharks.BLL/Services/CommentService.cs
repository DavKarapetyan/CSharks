using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _uow;
        public CommentService(ICommentRepository commentRepository, IUnitOfWork uow)
        {
            _commentRepository = commentRepository;
            _uow = uow;
        }

        public void Add(CommentAddEditVM model)
        {
            Comment comment= new Comment()
            {
                DateOfComment= DateTime.Now,
                CommentText= model.CommentText,
                Email= model.Email,
                Id= model.Id,
                Name= model.Name,
                NewsId= model.NewsId,
            };
            _commentRepository.Add(comment);
            _uow.Save();
        }

        public void Delete(int Id)
        {
           _commentRepository.Delete(Id);
            _uow.Save();
        }

        public CommentVM GetComment(int Id)
        {
            var comment = _commentRepository.GetById(Id);
            CommentVM commentVM = new CommentVM
            {
                DateOfComment = DateTime.Now,
                CommentText = comment.CommentText,
                Email = comment.Email,
                Id = Id,
                Name = comment.Name
            };
            return commentVM;
        }

        public List<CommentVM> GetCommentsByNewsId(int NewsId)
        {
            var comment = _commentRepository.GetAll().Where(c => c.Id == NewsId).Select(c => new CommentVM() 
            {
                Id = c.Id,
                Name= c.Name,
                CommentText= c.CommentText,
                DateOfComment= c.DateOfComment,
                Email= c.Email,
            }).ToList();
            return comment;
        }

        public CommentAddEditVM GetCommentForEdit(int Id)
        {
            var comment = _commentRepository.GetForEdit(Id);
            CommentAddEditVM commentaddeditVM = new CommentAddEditVM()
            {
                Id = Id,
                Name = comment.Name,
                CommentText = comment.CommentText,
                DateOfComment = comment.DateOfComment,
                Email = comment.Email,
                NewsId = comment.NewsId,
            };
            return commentaddeditVM;
        }

        public List<CommentVM> GetComments()
        {
           var comments=_commentRepository.GetAll().Select(c=>new CommentVM
           {
               Id = c.Id,
               Name = c.Name,
               CommentText= c.CommentText,
               DateOfComment= c.DateOfComment,
               Email = c.Email,
           }).ToList();
            return comments;
        }

        public void Update(CommentAddEditVM model)
        {
            var comment = new Comment()
            {
                Id = model.Id,
                Name = model.Name,
                CommentText = model.CommentText,
                DateOfComment = model.DateOfComment,
                Email = model.Email,
                NewsId = model.NewsId,
            };
            _commentRepository.Update(comment);
            _uow.Save();
        }
    }
}
