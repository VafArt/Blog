using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Application.Mappings;
using Blog.IdentityService.Domain.Errors;
using Blog.IdentityService.Domain.Repositories;

namespace Blog.IdentityService.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _userMapper;

        public GetUserByIdQueryHandler(IApplicationUserRepository userRepository, IUnitOfWork unitOfWork, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userMapper = userMapper;
        }

        public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellation)
        {
            var user = await _userRepository.GetUserByIdAsync(query.UserId);

            if (user is null) return Result.Failure(new GetUserByIdQueryResponse(), DomainErrors.ApplicationUser.NotFoundById(query.UserId));

            await _unitOfWork.SaveChangesAsync();

            return _userMapper.MapUserToGetUserByIdQueryResponse(user);
        }
    }
}
