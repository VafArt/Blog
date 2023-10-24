using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Application.Mappings;
using Blog.IdentityService.Domain.Errors;
using Blog.IdentityService.Domain.Repositories;

namespace Blog.IdentityService.Application.Users.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler : IQueryHandler<GetUserByNameQuery, Result<GetUserByNameQueryResponse>>
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _userMapper;

        public GetUserByNameQueryHandler(IApplicationUserRepository userRepository, IUnitOfWork unitOfWork, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userMapper = userMapper;
        }

        public async Task<Result<GetUserByNameQueryResponse>> Handle(GetUserByNameQuery query, CancellationToken cancellation)
        {
            var user = await _userRepository.GetUserByNameAsync(query.UserName);

            if (user is null) return Result.Failure(new GetUserByNameQueryResponse(), DomainErrors.ApplicationUser.NotFoundByName(query.UserName));

            await _unitOfWork.SaveChangesAsync(cancellation);

            return _userMapper.MapUserToGetUserByNameQueryResponse(user);
        }
    }
}
