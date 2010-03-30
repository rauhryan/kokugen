using System;
using Kokugen.Core;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Board
{
    public class SaveColumnAction
    {
        private readonly IProjectService _projectService;
        private readonly IBoardService _boardService;

        public SaveColumnAction(IProjectService projectService, IBoardService boardService)
        {
            _projectService = projectService;
            _boardService = boardService;
        }

        public AjaxResponse SaveColumn(BoardColumnInputModel model)
        {
            if (model.ProjectId.IsEmpty()) return new AjaxResponse { Success = false };

            var column = _boardService.ModifyColumn(model.ProjectId, model.ColumnId, model.ColumnName, model.ColumnDescription, model.ColumnLimit);

            return new AjaxResponse
            {
                Success = true,
                Item = column
            };
        }
    }

    public class BoardColumnInputModel
    {
        public Guid ProjectId { get; set; }
        public Guid ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnLimit { get; set; }
        public string ColumnDescription { get; set; }
    }
}