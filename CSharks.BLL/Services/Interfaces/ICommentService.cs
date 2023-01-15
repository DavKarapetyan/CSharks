using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;

namespace CSharks.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentVM> GetComments();
        public List<CommentVM> GetCommentsByNewsId(int NewsId);
        public CommentVM GetComment(int Id);
        public CommentAddEditVM GetCommentForEdit(int Id);
        void Add(CommentAddEditVM model);
        void Update(CommentAddEditVM model);
        void Delete(int Id);
    }
}
