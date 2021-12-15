using AutoMapper;
using Comments.Domain.CommentAggregate;
using Comments.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> GetAsync()
        {
            var comments = await _unitOfWork.CommentRepository.GetAsync();
            return _mapper.Map<List<Comment>, List<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetAsync(int commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetAsync(commentId);
            return _mapper.Map<Comment, CommentDto>(comment);
        }
    }
}
