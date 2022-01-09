using AutoMapper;
using Comments.Domain.CommentAggregate;
using Comments.Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CommentDto>> GetByChargingStationAsync(int chargingStationId, int? userId = null)
        {
            var endpoint = $"endpoint GET /Comments?chargingStationId={chargingStationId}&userId={userId}";
            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comments = await _unitOfWork.CommentRepository.GetAsync();
                comments = comments.Where(x => x.ChargingStationId == chargingStationId).ToList();

                if (userId != null)
                {
                    var comment =
                        comments.FirstOrDefault(x => x.ChargingStationId == chargingStationId && x.UserId == userId);
                    comments = new List<Comment>();
                    if (comment != null)
                    {
                        comments.Add(comment);
                    }
                }
                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return _mapper.Map<List<Comment>, List<CommentDto>>(comments);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }

        public async Task<CommentDto> GetAsync(int commentId)
        {
            var endpoint = $"endpoint GET /Comments/{commentId}";

            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comment = await _unitOfWork.CommentRepository.GetAsync(commentId);

                if (comment == null)
                {
                    _logger.LogInformation($"Exited {endpoint} with: 404 Comment with Id {commentId} not found");
                    return null;
                }

                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return _mapper.Map<Comment, CommentDto>(comment);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }

        public async Task<CommentDto> PostAsync(CommentPostDto commentPostDto)
        {
            var endpoint = $"endpoint POST /Comments";
            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comment = _mapper.Map<CommentPostDto, Comment>(commentPostDto);
                var addedComment = await _unitOfWork.CommentRepository.AddAsync(comment);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return _mapper.Map<Comment, CommentDto>(addedComment);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }

        public async Task<CommentDto> PutAsync(int commentId, CommentPutDto commentPutDto)
        {
            var endpoint = $"endpoint PUT /Comments/{commentId}";
            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comment = await _unitOfWork.CommentRepository.GetAsync(commentId);

                if (comment == null)
                {
                    _logger.LogInformation($"Exited {endpoint} with: 404 Comment with Id {commentId} not found");
                    return null;
                }

                comment.Content = commentPutDto.Content;
                comment.Rating = commentPutDto.Rating;
                comment.CreatedAt = DateTime.Now;

                var updatedComment = _unitOfWork.CommentRepository.Update(comment);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return _mapper.Map<Comment, CommentDto>(updatedComment);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            var endpoint = $"endpoint DELETE /Comments/{commentId}";
            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comment = await _unitOfWork.CommentRepository.GetAsync(commentId);

                if (comment == null)
                {
                    _logger.LogInformation($"Exited {endpoint} with: 404 Comment with Id {commentId} not found");
                    return false;
                }

                _unitOfWork.CommentRepository.Remove(commentId);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }
    }
}
